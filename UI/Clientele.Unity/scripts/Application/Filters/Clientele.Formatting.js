angular.module('Clientele.Formatting', ['ReferenceNumbers'])
.filter('fileSize', function () {
    var formatFileSizeUnits = function (bytes) {
        var i = -1;
        var byteUnits = [' kB', ' MB', ' GB', ' TB', 'PB', 'EB', 'ZB', 'YB'];
        do {
            bytes = bytes / 1024;
            i++;
        } while (bytes > 1024);

        if (i > 7) {
            return "Too large..";
        }
        return Math.max(bytes, 0.1).toFixed(1) + byteUnits[i];
    };

    return function (input) {
        // input will be the string we pass in
        if (input) {
            return formatFileSizeUnits(input);
        }
    };
})
    .filter('startFrom', function () {
        return function (input, start) {
            start = +start; //parse to int
            return input.slice(start);
        };
    })
    .filter('saCurrency', function () {
        return function (input) {
            if (input === null) {
                return 'R ' + parseFloat(0).toFixed(2);
            };
            return 'R ' + parseFloat(input).toFixed(2);
        };
    })
    .filter('stdDateTime', function ($filter) {
        return function (input) {
            if (input == null) { return ""; }

            var date = $filter('date')(new Date(input), 'dd MMMM yyyy hh:mm:ss a');

            return date;
        };
    })
    .filter('stdDate', function ($filter) {
          return function (input) {
              if (input == null) { return ""; }

              var date = $filter('date')(new Date(input), 'dd MMMM yyyy');

              return date;
          };
      })
    .filter('boolYesNo', function () {
        return function (input) {
            if (input === true) {
                return "Yes";
            } else if (input === false) {
                return "No";
            } else {
                return "Invalid Input";
            }

        };
    })
    .filter('emptyString', function () {
        return function (input) {
            if (input !== undefined && input !== "") {
                return input;
           } else {
                return "Empty";
           }
        };
    });

angular.module('ReferenceNumbers', [])
    .filter('referenceNumber', function () {

        return function (input, format) {
            if (input) {
                if (format.prefix == "undefined" || format.prefix == undefined) {
                    format.prefix = "";
                }

                if (format.length == "undefined" || format.length == undefined) {
                    format.length = 8;
                }

                var formattedString = String(input);
                formattedString = formattedString.replace(format.prefix.toLowerCase(), "");
                formattedString = formattedString.replace(format.prefix.toUpperCase(), "");

                if (formattedString.length < format.length) {
                    var leadingZeroCount = format.length - formattedString.length;
                    for (var i = 0; i < leadingZeroCount; i++) {
                        formattedString = "0" + formattedString;
                    }
                }

                formattedString = format.prefix + formattedString;

                return formattedString;
            }

            return "";
        };
    });
