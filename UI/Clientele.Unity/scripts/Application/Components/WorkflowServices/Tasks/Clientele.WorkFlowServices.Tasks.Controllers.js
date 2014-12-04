/**********************************************************************************************/
(function () {

    var dashBoardController = function ($scope, $location, uiLoader, tasksWebApi, taskService, taskApplicationRepository, applicationId, unityApplicationRepository) {

        var determineApplicationFromPath = function () {
            var splitPath = $location.$$path.split("/");

            if (splitPath.length > 0) {
                var applicationKey = splitPath[1];
                return applicationKey.toLowerCase();
            }
        };

        $scope.tasks = [];
        $scope.currentPage = 0;
        $scope.maxSize = 5;
        $scope.itemsPerPage = 0;
        $scope.availableCount = 0;
        // $scope.taskService = taskService;

        $scope.breadcrumbList = [];

        $scope.buildHistoryItems = function () {
            $scope.historyItems = $scope.breadcrumbList.slice(0, $scope.breadcrumbList.length - 1);
            $scope.activeBreadCrumb = $scope.breadcrumbList[$scope.breadcrumbList.length - 1];
        }

        $scope.activateBreadCrumb = function (item) {
            item.Action();

            var index = $scope.breadcrumbList.indexOf(item);
            $scope.breadcrumbList = $scope.breadcrumbList.slice(0, index + 1);
            $scope.buildHistoryItems();
        }

        $scope.refresh = function () {
            if ($scope.activeBreadCrumb.Refresh == null)
                return;

            $scope.activeBreadCrumb.Refresh();
        }

        function addHistoryItem(name, backAction, level, refreshAction) {

            if ($scope.breadcrumbList.length > 0 && $scope.breadcrumbList[$scope.breadcrumbList.length - 1].Level == level) {
                $scope.breadcrumbList[$scope.breadcrumbList.length - 1] = { Name: name, Action: backAction, Level: level, Refresh: refreshAction };
            }
            else
                $scope.breadcrumbList.push({
                    Name: name, Action: backAction, Level: level, Refresh: refreshAction
                });

            $scope.buildHistoryItems();
        }

        $scope.availableTasks = function () {
            return $scope.availableCount > 0;
        };

        $scope.hasItems = function (item) {
            return item.Total > 0;
        };

        $scope.hasNoItems = function (item) {
            return item.Total == 0;
        };

        $scope.taskQueueHasItems = function (item) {
            return item.ActiveCount > 0;
        };

        $scope.taskQueueHasNoItems = function (item) {
            return item.ActiveCount == 0;
        };

        $scope.postponeTask = function (item) {
            taskService.postponeTask(item);
        };

        $scope.getFieldValue = function (array, key) {
            return taskService.getFieldByKey(array, key);
        };

        $scope.claimTask = function (queue) {
            uiLoader.UseWithLoader($scope, function () { return tasksWebApi.claimTask(queue.TaskQueueName); }, function (data) {
                navigateToTask(data.TaskId, queue);
            });
        };

        $scope.setPage = function (pageNo) {

            if ($scope.selectedQueue == {} || pageNo == 0)
                return;

            loadTaskList(pageNo);
        };

        $scope.shouldShowTaskList = function () {
            return $scope.selectedQueue != null && $scope.selectedQueueData != undefined && $scope.selectedQueueData.length > 0;
        };

        $scope.shouldShowEmptyTaskList = function () {
            return $scope.selectedQueue != null && ($scope.selectedQueueData == undefined || $scope.selectedQueueData.length == undefined || $scope.selectedQueueData.length == 0);
        };

        $scope.noWorkForQueue = function () {
            return $scope.selectedQueueData.length == 0 && $scope.selectedQueue != {};
        };

        $scope.selectTask = function (task) {
            navigateToTask(task.Id);
        };

        $scope.isSelected = function (value) {
            return value == $scope.selectedQueue.Key;
        };

        $scope.selectQueue = function (queue) {
            $scope.selectedQueue = queue;
            loadTaskList(1);
            addHistoryItem(queue.Status, clearSelectedTaskQueue, "Queue");
        };

        $scope.selectTaskQueue = function (item) {
            $scope.selectedTaskQueue = item;
            loadQueueList();
            addHistoryItem(item.TaskTemplateName, clearSelectedTaskQueueData, "TaskQueue");
        };

        $scope.selectRole = function (item) {
            $scope.selectedRole = item;
            $scope.hasClaimableQueues = Enumerable.From(item.TaskBaskets).Any(function (x) {
                return x.TaskResponsibilityId != 7;
            });
            addHistoryItem(item.Role, clearSelectedTaskQueue, "Role", getTaskBasket);
        };

        function clearSelectedRole() {
            $scope.selectedRole = null;
            clearSelectedTaskQueue();
        };

        function clearSelectedTaskQueue() {
            $scope.selectedTaskQueue = null;
            $scope.selectedQueueData = null;
            $scope.selectedQueue = null;
        };

        function clearSelectedTaskQueueData() {
            $scope.selectedQueueData = null;
            $scope.selectedQueue = null;
        };

        function loadTaskList(pageNo) {

            if (angular.isUndefined($scope.selectedTaskQueue) || angular.isUndefined($scope.selectedQueue) || angular.isUndefined($scope.selectedQueue.Key))
                return;

            if (angular.isUndefined($scope.pageNo))
                $scope.pageNo = 1;
            else
                $scope.pageNo = pageNo;

            var action = function () { return tasksWebApi.loadTaskList($scope.selectedTaskQueue.TaskQueueName, $scope.selectedTaskQueue.RoleId, $scope.selectedQueue.Key, $scope.pageNo); };
            var resultAction = function (data) {
                var fields = taskApplicationRepository.getTaskDisplayFields(determineApplicationFromPath(), $scope.selectedTaskQueue.TaskQueueName);
                $scope.taskQueueFields = fields;

                $scope.selectedQueueData = tasksWebApi.filter(data.Results);
                $scope.totalItems = data.TotalResultCount;
                $scope.itemsPerPage = data.PageSize;
            };

            uiLoader.UseWithLoader($scope, action, resultAction);
        }

        function navigateToTask(taskId, queue) {

            var taskQueueName = queue == null ? $scope.selectedTaskQueue.TaskQueueName : queue.TaskQueueName;
            var route = taskApplicationRepository.getRoute(determineApplicationFromPath(), taskQueueName);
            $location.path(route + taskId);
        };

        function getTaskBasket() {
            uiLoader.UseWithLoader($scope, tasksWebApi.getTaskBasket, function (data) {
                $scope.userBaskets = data;

                if (data.length == 1)
                    $scope.selectRole(data[0]);
            });
        }

        function loadQueueList() {
            if (angular.isUndefined($scope.selectedTaskQueue) || $scope.selectedTaskQueue == null) {
                return;
            }
            uiLoader.UseWithLoader($scope, function () { return tasksWebApi.loadQueueList($scope.selectedTaskQueue.TaskQueueName, $scope.selectedTaskQueue.RoleId); }, function (data) {
                $scope.taskStatusList = taskService.filterQueue(determineApplicationFromPath(), data, $scope.teamMember.Role);
            });
        }

        var success = function (data) {
            addHistoryItem('Home', clearSelectedRole, "Home");
            $scope.teamMember = data;
            getTaskBasket();
        };

        var error = function () {
            locationService.Unauthorized();
        };

        uiLoader.UseWithLoader($scope, tasksWebApi.getTeamMember, success, error);
    }

    dashBoardController.$inject = [
        '$scope', '$location', 'uiLoader', 'Clientele.WorkFlowServices.Tasks.Services.WebApiService',
        'Clientele.WorkFlowServices.Tasks.Services.ApplicationService', 'Clientele.WorkFlowServices.Tasks.Services.ApplicationRepository', 'applicationId', 'unityApplicationRepository'];

    angular.module('Clientele.WorkFlowServices.Tasks.Controllers', [])
        .controller("WorkflowServices.Tasks.Controllers.DashboardController", dashBoardController)
        .controller('WorkflowServices.Tasks.Controllers.TaskActionController', [
            '$scope',
            '$routeParams',
            '$rootScope',
            'recordStoreApplicationApiUrl',
            'Clientele.WorkFlowServices.Tasks.Services.ApplicationService',
            'uiLoader',
            'eventBroadcastingService',
            'Clientele.WorkFlowServices.Tasks.ModalService',
            function ($scope,
                $routeParams,
                $rootScope,
                recordStoreApplicationApiUrl,
                taskService,
                uiLoader,
                eventBroadcastingService,
                taskModalService
            ) {

                //,
                //locationService,
                //modalService

                //var configuration = applicationHost.retrieveApplicationConfigurationById('Clientele.ApplicationFormsCapture');
                //$scope.sourceUrl = configuration.UnityUrl;

                $scope.taskService = taskService;

                var claimTaskAction = function () {
                    return taskService.claimTask($scope.task);
                };

                var acceptTaskAction = function () {
                    return taskService.acceptTask($scope.task.Id);
                };

                var resumeTaskAction = function () {
                    return taskService.resumeTask($scope.task.Id);
                };

                var customTasks = [];
                customTasks.push({
                    Label: "Save",
                    Action: function(task) {
                        alert(task.Id);
                    },
                    CanPerformAction : function(task) {
                        return true;
                    }
                });
                customTasks.push({
                    Label: "Save2",
                    Action: function (task) {
                        alert(task.Id);
                    },
                    CanPerformAction: function (task) {
                        return true;
                    }
                });

                $scope.customTasks = customTasks;

                $scope.$on("Clientele.WorkflowServices.ClaimSuccess", function () {
                    loadTask();
                });

                $scope.$on("Clientele.WorkflowServices.AcceptSuccess", function () {
                    loadTask();
                });

                $scope.$on("Clientele.WorkflowServices.ResumeSuccess", function () {
                    loadTask();
                });

                $scope.$on("Clientele.WorkflowServices.AfterPostponeTaskComplete", function () {
                    loadTask();
                });

                $scope.$on("Clientele.WorkflowServices.RevokeSuccess", function () {
                    loadTask();
                });

                $scope.$on("Clientele.WorkflowServices.DelegateSuccess", function () {
                    loadTask();
                });

                $scope.$on("Clientele.WorkflowServices.AssignSuccess", function () {
                    loadTask();
                });
                

                function loadTask() {
                    var action = function () { return taskService.loadTask($routeParams.taskId); };
                    uiLoader.UseWithLoader($scope, action, function (data) {
                        $scope.isReadonly = data.Status != "In Progress";
                        $scope.task = data;
                        configureAvailableActions(data.PermittedActions);
                        $scope.pdfUrl = "/Scripts/UI/jsPDF/web/viewer.html?token=" + $rootScope.BearerToken + "&file=" + recordStoreApplicationApiUrl + "/RecordFile/" + $scope.task.ResourceId;
                        //loadData();
                    });
                }

                function configureAvailableActions(permittedActions) {
                    $scope.canSubmit = taskService.userCanPerformTask(permittedActions, 'Submit');

                    var allowedActions = { allowedActionCount: 0 };
                    $scope.canClaim = taskService.userCanPerformTask(permittedActions, 'Claim', allowedActions);
                    $scope.canPostpone = taskService.userCanPerformTask(permittedActions, 'Postpone', allowedActions);
                    $scope.canAccept = taskService.userCanPerformTask(permittedActions, 'Accept', allowedActions);
                    $scope.canRevoke = taskService.userCanPerformTask(permittedActions, 'Revoke', allowedActions);
                    $scope.canResume = taskService.userCanPerformTask(permittedActions, 'Resume', allowedActions);
                    $scope.canAssign = taskService.userCanPerformTask(permittedActions, 'Assign', allowedActions);
                    $scope.canDelegate = taskService.userCanPerformTask(permittedActions, 'Delegate', allowedActions);

                    $scope.allowedActionCount = allowedActions.allowedActionCount;
                }

                var init = function () {

                    var action = function () { return taskService.loadTask($routeParams.taskId); };
                    uiLoader.UseWithLoader($scope, action, function (data) {
                        $scope.isReadonly = data.Status != "In Progress";
                        $scope.task = data;
                        configureAvailableActions(data.PermittedActions);

                        eventBroadcastingService.broadcastEvent("Clientele.WorkflowServices.TaskLoaded", data);

                        //  $scope.pdfUrl = "/Scripts/UI/jsPDF/web/viewer.html?token=" + $rootScope.BearerToken + "&file=" + recordStoreApplicationApiUrl + "/RecordFile/" + $scope.task.ResourceId;

                    });
                };

                init();

            }
        ])
        .controller('WorkflowServices.Tasks.Controllers.TaskOwnershipModalController', [
            '$scope', '$modalInstance', 'model', function ($scope, $modalInstance, model) {

                $scope.model = model;

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
            }
        ]).controller('WorkflowServices.Tasks.Controllers.StatusHistoryModalController', [
            '$scope', '$modalInstance', 'model', function ($scope, $modalInstance, model) {

                $scope.model = model;

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
            }
        ]).controller('WorkflowServices.Tasks.Controllers.TaskCommentsController', [
            '$scope', '$modalInstance', 'model', function ($scope, $modalInstance, model) {

                $scope.model = { Comments: model };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
            }
        ]).controller('WorkflowServices.Tasks.Controllers.DelegateTaskModalController',
             ['$scope', '$modalInstance', 'model', 'ajaxJsonService', function ($scope, $modalInstance, model, ajaxJsonService) {

                 $scope.teams = {};

                 $scope.selectTeam = function () {
                     $scope.selectedTeamMembers = $scope.model.selectedTeam.TeamMembers;
                 };

                 ajaxJsonService.Get(model.ResourceUri).then(function (data) {
                     $scope.teams = data.data;
                 });

                 $scope.header = 'Delegate Task';
                 $scope.label = 'Comment';
                 $scope.model = {};
                 $scope.model.Reasons = model.Reasons;

                 $scope.ok = function () {
                     $modalInstance.close({ comment: $scope.model.InputValue, reason: $scope.model.Reason.Id, userId: $scope.model.selectedUser.Id });
                 };

                 $scope.cancel = function () {
                     $modalInstance.dismiss('cancel');
                 };
             }])
        .controller('WorkflowServices.Tasks.Controllers.CommentModalController', ['$scope', '$modalInstance', 'model', function ($scope, $modalInstance, model) {

            $scope.header = model.Header;

            $scope.label = model.Label;
            $scope.model = model;

            $scope.ok = function () {
                $modalInstance.close($scope.model.comment);
            };

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };
        }
        ])
        .controller('Clientele.Tasks.Controllers.PostponeCaptureTaskController', ['$scope', '$modalInstance', 'model', function ($scope, $modalInstance, model) {

        $scope.model = {};
        $scope.model.Comment = "";

        $scope.Reasons = model.Reasons;
        $scope.model.Reason = $scope.Reasons[0];
        $scope.model.Date = new Date();
        $scope.model.Time = new Date();
        $scope.minDate = new Date();

        $scope.disabled = function (date, mode) {
            return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };

        $scope.dateTimeChanged = function () {
            $scope.model.datetime = new Date($scope.model.Date.getFullYear(), $scope.model.Date.getMonth(), $scope.model.Date.getDate(),
                $scope.model.Time.getHours(), $scope.model.Time.getMinutes(), 0);
        };

        $scope.save = function () {
            $modalInstance.close({ DateTime: $scope.model.datetime, Comment: $scope.model.Comment, Reason: $scope.model.Reason.Id });
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }])
        .controller('WorkflowServices.Tasks.Controllers.GenericInputModalController', ['$scope', '$modalInstance', 'model', function ($scope, $modalInstance, model) {

        $scope.header = model.Header;
        $scope.label = model.Label;
        $scope.model = model;

        $scope.ok = function () {
            $modalInstance.close($scope.model.InputValue);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]).controller('WorkflowServices.Tasks.Controllers.AssignTaskModalController', ['$scope', '$modalInstance', 'model', 'ajaxJsonService', function ($scope, $modalInstance, model, ajaxJsonService) {

        $scope.header = model.Header;
        $scope.teams = {};

        $scope.selectTeam = function () {
            $scope.selectedTeamMembers = $scope.model.selectedTeam.TeamMembers;
        };

        ajaxJsonService.Get(model.ResourceUri).then(function (data) {
            $scope.teams = data.data;
        });

        $scope.label = model.Label;
        $scope.model = model;

        $scope.ok = function () {
            $modalInstance.close($scope.model.selectedUser.Id);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

})();

//    , 'Clientele.ApplicationFormsCapture.Common.Services.TaskService'
//    , 'Clientele.ApplicationFormsCapture.Services.RouteManager'
//    , 'Clientele.ApplicationFormsCapture.Services.TeamMemberService'
//    , 'Clientele.ApplicationFormsCapture.Services.LocationService'


