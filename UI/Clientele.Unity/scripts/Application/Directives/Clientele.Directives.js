/**********************************************************************************************/
/* Module Definition - directives                                                             */
/**********************************************************************************************/

//var buildNumber = configuration.BuildNumber;

angular.module('Clientele.Directives', ['Clientele.Directives.FormFields'])
    .directive('applicationMode', function (runningMode) {
        return {
            restrict: 'AE',
            replace: true,
            transclude: true,
            template: runningMode == "Production" ? '<div class="alert-info well-sm" align="center">You are currently in ' + runningMode + ' Mode</div>' : '<div class="alert-danger well-sm" align="center">You are currently in ' + runningMode + ' Mode</div>'
        };
    })
    .directive('numbersOnly', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, modelCtrl) {
                modelCtrl.$parsers.push(function (inputValue) {
                    // this next if is necessary for when using ng-required on your input. 
                    // In such cases, when a letter is typed first, this parser will be called
                    // again, and the 2nd time, the value will be undefined
                    if (inputValue == undefined) return '';
                    var transformedInput = inputValue.replace(/[^0-9]/g, '');
                    if (transformedInput != inputValue) {
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                    }

                    return transformedInput;
                });
            }
        };
    })
    .directive('dropdown', function ($compile) {

        function link(scope, element, attrs) {

        }

        return {
            restrict: 'A',
            replace: false,
            transclude: true,
            template:
                    '<ui-select ng-model="dropdownModel.selectedItem">' +
                    '<ui-select-match placeholder="Select">{{$select.selected.Text}}</ui-select-match>' +
                    '<ui-select-choices repeat="item in dropdownModel.options | filter: $select.search">' +
                    '<div ng-bind-html="item.Text | highlight: $select.search"></div>' +
                    '</ui-select-choices>' +
                    '</ui-select>',
            priority: 1000,
            scope: {
                dropdownModel: '=',
            },
            link: link
        };
    })
    .directive('emptyResults', function ($compile, $parse) {
        return {
            restrict: 'AE',
            replace: false,
            terminal: true,
            priority: 1000,

            compile: function ($element) {

                $element.html("<div ng-hide='isEmpty'>" + $element.html() + "</div>");

                var emptyResultSetHtml = "<div ng-show='isEmpty'><h4>{{emptyMessage}}</h4></div>";

                $element.html(emptyResultSetHtml + $element.html());

                $element.removeAttr("empty-results"); //remove the attribute to avoid infinite loop
                $element.removeAttr("data-empty-results"); //remove the attribute to avoid infinite loop

                return {
                    pre: function preLink(scope, iElement, iAttrs, controller) {

                        if (iAttrs["emptyMessage"]) {
                            scope.emptyMessage = iAttrs["emptyMessage"];
                        }

                        if (iAttrs["emptyCollection"]) {
                            var coll = iAttrs["emptyCollection"];

                            scope.isEmpty = eval('scope.' + coll).length == 0;

                            scope.$watch(iAttrs["emptyCollection"], function () {
                                scope.isEmpty = eval('scope.' + coll).length == 0;
                            });
                        }

                    },
                    post: function postLink(scope, iElement, iAttrs, controller) {
                        $compile(iElement)(scope);
                    }
                };
            }
        };
    })
    .directive('loadingWidget', function ($compile) {
        return {
            restrict: 'AE',
            replace: false,
            terminal: true,
            priority: 1000,

            compile: function ($element) {

                $element.html("<div ng-show='showContent'>" + $element.html() + "</div>");

                var errorAndLoadHtml = "<div ng-show='loadError'>There was an error loading your content, please contact an administrator.</div>";
                errorAndLoadHtml += '<div ng-show="loading" class="row-fluid ui-corner-all" style="padding: 0 .7em;">';
                errorAndLoadHtml += '<div class="loadingContent"><p><img alt="Loading  Content" src="/Content/ajax-loader.gif" />&nbsp;{{LoadingMessage}}</p></div>';
                errorAndLoadHtml += '</div>';

                $element.html(errorAndLoadHtml + $element.html());

                $element.removeAttr("loading-widget"); //remove the attribute to avoid infinite loop
                $element.removeAttr("data-loading-widget"); //remove the attribute to avoid infinite loop

                return {
                    pre: function preLink(scope, iElement, iAttrs, controller) {
                        var realScope = scope;

                        if (iAttrs["isInclude"]) {
                            realScope = scope.$parent;
                        }

                        realScope.showContent = false;

                        if (iAttrs["showContent"]) {
                            if (eval(iAttrs["showContent"])) {
                                realScope.showContent = true;
                            }
                        }

                        realScope.loadEmpty = false;
                        realScope.loading = false;
                        realScope.loadError = false;
                    },
                    post: function postLink(scope, iElement, iAttrs, controller) {

                        $compile(iElement)(scope);
                    }
                };
            }
        };
    })
    .directive('manualLoadingWidget', function ($compile) {
        return {
            restrict: 'AE',
            replace: false,
            terminal: true,
            priority: 1000,

            compile: function ($element) {

                $element.html("<div ng-show='manualShowContent'>" + $element.html() + "</div>");

                var errorAndLoadHtml = "<div ng-show='loadError'>There was an error loading your content, please contact an administrator.</div>";
                errorAndLoadHtml += '<div ng-show="manualLoading" class="row-fluid ui-corner-all" style="padding: 0 .7em;">';
                errorAndLoadHtml += '<div class="loadingContent"><p><img alt="Loading  Content" src="/Content/ajax-loader.gif" />&nbsp;{{LoadingMessage}}</p></div>';
                errorAndLoadHtml += '</div>';

                $element.html(errorAndLoadHtml + $element.html());

                $element.removeAttr("manual-loading-widget");

                return {
                    pre: function preLink(scope, iElement, iAttrs, controller) {
                        var realScope = scope;

                        realScope = scope.$parent;

                        realScope.manualShowContent = false;

                        realScope.manualLoading = false;
                        realScope.loadError = false;
                    },
                    post: function postLink(scope, iElement, iAttrs, controller) {

                        $compile(iElement)(scope);
                    }
                };
            }
        };
    })
    .directive('authoriseAccess', function (authenticationService) {
        var hasAccessCheck = function (requiredClaims) {

            for (var i = 0; i < requiredClaims.length; i++) {
                if (authenticationService.HasClaim(requiredClaims[i])) {
                    return true;
                }
            }

            return false;
        };

        return {
            restrict: 'AE',
            scope: {},
            link: function ($scope, element, attrs) {

                if (attrs.claimRequired == "")
                    return;

                var claims = attrs.claimRequired.split(",");

                var hasAccess = hasAccessCheck(claims);

                if (!hasAccess) {
                    if (eval(attrs.redirect)) {
                        authenticationService.RedirectToNoAccessPage();
                        return;
                    }

                    var accessMessage = "";

                    if (!eval(attrs.suppressMessage)) {
                        accessMessage = "<b>You do not have access to " + attrs.sectionname + ", please contact an administrator.</b>";
                    }

                    element.replaceWith(accessMessage);

                    $scope.$$nextSibling.$destroy();
                }
            }
        };
    })
     .directive('unityAddressView', function () {
         return {
             restrict: 'E',
             templateUrl: "Views/UnityAddress/View.html",
             scope: {
                 addressType: '=?',
                 addressLine1: '=?',
                 addressLine2: '=?',
                 addressLine3: '=?',
                 addressLine4: '=?',
                 postalCode: '=?',
                 enableEdit: '=?',
                 loadEvent: '=?',
                 cacheData: '=?',
                 dataLoadedEmitEvent: '=?',
                 overrideTemplate: '=?'
             },
             controller: 'UnityAddressViewController',
             compile: function (tElement, tAtrrs) {
                 if (!angular.isUndefined(tAtrrs.overrideTemplate)) {
                     if (tAtrrs.overrideTemplate != "") {
                         if (eval(tAtrrs.overrideTemplate)) {
                             tElement.html("");
                         }
                     }
                 }
             }
         };
     })
    .directive('unityBankDetailView', function () {
        return {
            restrict: 'E',
            templateUrl: 'Views/UnityBankDetails/View.html',
            scope: {
                bankAccountId: '=?',
                accountType: '=?',
                accountTypeId: '=?',
                accountName: '=?',
                accountNumber: '=?',
                bankName: '=?',
                branchCode: '=?',
                branchCodeName: '=?',
                enableEdit: '=?',
                isPayable: '=?',
                loadEvent: '=?',
                cacheData: '=?',
                dataLoadedEmitEvent: '=?',
                overrideTemplate: '=?'
            },
            controller: 'UnityBankDetailViewController',
            compile: function (tElement, tAtrrs) {
                if (!angular.isUndefined(tAtrrs.overrideTemplate)) {
                    if (tAtrrs.overrideTemplate != "") {
                        if (eval(tAtrrs.overrideTemplate)) {
                            tElement.html("");
                        }
                    }
                }
            }
        };
    })
    .directive("outsideClick", ['$document', '$parse', function ($document, $parse) {
        return {
            link: function ($scope, $element, $attributes) {
                var scopeExpression = $attributes.outsideClick,
                    onDocumentClick = function (event) {
                        var isChild = $element.find(event.target).length > 0;

                        if (!isChild) {
                            $scope.$apply(scopeExpression);
                        }
                    };

                $document.on("click", onDocumentClick);

                $element.on('$destroy', function () {
                    $document.off("click", onDocumentClick);
                });
            }
        }
    }]);
