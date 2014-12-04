(function () {

    var additionalRequiredScripts = [],
        applicationId = "Clientele.MyComponent",
        application = applicationHost.retrieveApplicationById(applicationId),
        configuration = applicationHost.retrieveApplicationConfigurationById(applicationId),
        sourceUrl = configuration.UnityUrl,
        buildNumber = configuration.BuildNumber;

    // list of scripts to load using the url on the server that the application is hosted on
    additionalRequiredScripts.push(sourceUrl + "scripts/" + applicationId + ".Routes.js?buildNumber=" + buildNumber);
    additionalRequiredScripts.push(sourceUrl + "scripts/" + applicationId + ".Services.js?buildNumber=" + buildNumber);
    additionalRequiredScripts.push(sourceUrl + "scripts/" + applicationId + ".Controllers.js?buildNumber=" + buildNumber);
    additionalRequiredScripts.push(sourceUrl + "scripts/" + applicationId + ".Directives.js?buildNumber=" + buildNumber);

    // Function used to define the Angular Module
    // it is wrapped in a function as the script loading mechanism requires a function to be executed once the scripts are all "in memory"
    var angularModuleDefinitionAction = function () {

        // module definition with array of module dependencies
        angular.module(applicationId, ['Clientele.MyComponent.Routes', 'Clientele.MyComponent.Directives'])

            // run occurs once on the construction of your module
            // the unityApplicationRepository is injected to register your application
             .run(function (unityApplicationRepository) {

                 // applicationId - unique guide you will want to give to your application (just generate one - we won't have that many to warrant sequential)

                 // componentConfiguration:
                 // - applicationGuid
                 // - searchUrl ( leave blank if you are not expecting a main UI search box ) [route must be defined in your routes module]
                 // - applicationName - used for navigation on main application menu
                 // - icon - currently not being used, but could be in future - leave blank
                 // - IdentityPrefix - default claim/right/permission required for access to this application - blank means EVERYONE has access 
                 // - WorkFlowServiceApiUrl - environment variable name pointing to workflow services - typically your application api 

                 var applicationGuid = "a179f72d-dd66-cae9-895d-08d0bd6e23ed",
                     componentConfiguration = { Id: applicationGuid, searchUrl: "/MyComponent/UnitySearch/", applicationName: "MyComponent", icon: "", IdentityPrefix: "Clientele.MyComponent", WorkFlowServiceApiUrl: "encashmentApiUrl" },
                     titleBarNavigation = [];


                 // titleBarNavigation - custom navigation used when your application is active
                 // url - hashbang'd route to used for the link [route must be defined in your routes module]
                 // label - text used to show on the menu
                 // requiredClaim: minimum claim/right/permission to access the menu item - this will show/hide the menu item [comma delimited if needs an or check of rights]
                 // childNavItems - [] of second level navigation items 

                 titleBarNavigation.push({ url: '#/MyComponent/', label: 'My Component Home', requiredClaim: "" });

                 var childItems = [];
                 childItems.push({ url: '#/MyComponent/SubItem/', label: 'Component sub', requiredClaim: "" });

                 titleBarNavigation.push({ url: '#/MyComponent/Test', label: 'Test', requiredClaim: "", childNavItems: childItems });

                 // register application with no spaces in key - this is what will be used to determine application context
                 // when you swap applications
                 unityApplicationRepository.addApplication("MyComponent", titleBarNavigation, componentConfiguration);
             });

        // Tells Unity you are done loading anything you need to for the component
        applicationHost.completeApplicationRegistration(applicationId);
    }

    // this method is called by the UnityApplicationHost when bootstrapping all of the external plugins/applications
    var pluginRegistrationMethod = function () {

        // LazyLoad.js exists in the UnityApplication core and loads all the scripts in the array above in parallel
        // The second parameter is what is called when all the scripts are "loaded" allowing you to use all dependencies when needed
        LazyLoad.js(additionalRequiredScripts, angularModuleDefinitionAction);
    }

    // RegisterApplication is the convention used by Unity to register your plugin.. do not change the name of the application.RegisterApplication
    application.RegisterApplication = pluginRegistrationMethod;

})();