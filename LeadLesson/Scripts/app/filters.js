﻿(function () {
    angular.module('BridgeLessonModule').filter('Tto10', function () {
      return function (text) {
          return text ? text.replace('T', '10') : '';
      };
    });
        
    //We already have a limitTo filter built-in to angular,
    //let's make a startFrom filter
    angular.module('BridgeLessonModule').filter('startFrom', function () {
        return function (input, start) {
            start = +start; //parse to int
            return input ? input.slice(start) : input;
        }
    });

    var club_icon = '<img src="/Content/img/club-icon.png" />';
    var heart_icon = '<img src="/Content/img/heart-icon.png" />';
    var spade_icon = '<img src="/Content/img/spade-icon.png" />';
    var diamond_icon = '<img src="/Content/img/diamond-icon.png" />';
    //todo: directive and div xs-3 per each row?
    //format: 'N:1H;E:dbl,S:2NT'
    angular.module('BridgeLessonModule').filter('bidding', ['$sce',function ($sce) {
        return function (text) {
            if (!text)
                return text;
            text = replaceBidsOnPictures(text);
            var bids = text.split(';');
            //var result = '<table><tbody><tr><th>N</th><th>E</th><th>S</th><th>W</th>'
            var result = '<table><tbody><tr><th>N</th><th>S</th>'
            for (var i = 0; i < bids.length; i++) {
                if (i % 2 == 0)
                    result += '</tr><tr>';
                var bid = bids[i].split(':');
                bidToRender = bid.length > 1 ? bid[1] : bid[0];
                result += '<td>' + bidToRender + '</td>';
            }
            result += '</tr></tbody></table>';

            return $sce.trustAsHtml(result); 
        };
    }]);

    angular.module('BridgeLessonModule').filter('biddingSymbols', ['$sce', function ($sce) {
        return function (text) {
            if (!text)
                return text;
            result = replaceBidsOnPictures(text);
            return $sce.trustAsHtml(result);
        };
    }]);

    function replaceBidsOnPictures(text) {
        var a = text.replace(new RegExp('H!', 'g'), heart_icon).replace(new RegExp('C!', 'g'), club_icon);
        var c  = a.replace(new RegExp('S!', 'g'), spade_icon);
        return c.replace(new RegExp('D!', 'g'), diamond_icon);
    }

    function replaceAll(str, find, replace) {
        return str.replace(new RegExp(find, 'g'), replace);
    }

}());

