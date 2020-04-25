
//$('#searchButton').click(function ()
$('#q').keypress(function (event) {    
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        $("#mainContainer").hide();
        $("#searchResult").html("");
        $("#searchResult").show();
        var query = document.getElementById('q').value;
		if (typeof(query) == "undefined" || !query) return;
        var url = 'https://www.googleapis.com/customsearch/v1?key=AIzaSyCMGfdDaSfjqv5zYoS0mTJnOT3e9MURWkU&cx=017800740009285026140:WMX-1508934631&filter=0&q=' + query;

        var trackEvent = "GoogleCustomSearch";
        //_gaq.push(['_trackEvent', trackEvent, 'clickSearch']); //Category -trackUrl, Action - clickSearch
		var loc = "'" + document.location || 'notFound' + "'";
        ga('send', 'event', 'SearchEnter', query, loc, { 'nonInteraction': 1 });
        //_setCustomVar(index, name, value, opt_scope)
        //index (Required) -1 to 5, name (required) - name of cusom variable, value (required) - value of customVar,
        //opt_scope —Optional - The scope for the custom variable. the scope defines the level of user engagement with your site. 
        //possible values are 1 (visitor-level), 2 (session-level), or 3 (page-level). When left undefined, the custom variable scope defaults to page-level interaction.
        //_gaq.push(['_setCustomVar', 1, 'searchQuery', query, 1]);
        ga('set', '1', query);
        $.ajax({
            type: "GET",
            url: url,
            crossDomain: true,
            dataType: 'jsonp',
            success: function (data) {
                $("#searchResult").html("");

                $('#searchResult').append("<br />");

                if (data == null || data == 'undefined')
                    return;

                var totalResults = 0;
                if (data.items != null && data.items.length > 0)
                    totalResults = data.items.length;

                $('#searchResult').append("<span class='search-summary' >About " + totalResults + " results (" + data.searchInformation.searchTime.toFixed(2) + " seconds)</span>");
                $('#searchResult').append("<br />");

                if (totalResults == 0) {
                    $('#searchResult').append("<br />");
                    var notFound = "Your search <b>" + query + "</b>  did not match any documents." + "<br /><br />" +
                    "<u>Suggestions:</u>" + "<br />" +
                    "• Make sure that all words are spelled correctly." + "<br />" +
                    "• Try different keywords." + "<br />" +
                    "• Try more general keywords.";
                    $('#searchResult').append("<span class='search-result-norecord'>" + notFound + "</span>");
                    $('#searchResult').append("<br />");
                }

                if (data.items == null || data.items.length == 0)
                    return;

                for (var i = 0; i <= data.items.length; i++) {
                    var qr = data.items[i]; if (qr == null || qr == 'undefined')
                        continue;
                    $('#searchResult').append('<div id="r' + i + '" class="search-result-div">');
                    $("#searchResult").append("<a href='" + qr.link + "' target='_blank' class='search-result-url' ><span class='search-result-title'>" + qr.title + "</span></a>");
                    //$('#searchResult').append("<span class='search-result-title'>" + qr.title + "</span>");
                    $('#searchResult').append("<br />");
                    $('#searchResult').append("<a href='" + qr.link + "' class='search-result-url' >" + qr.formattedUrl + "</a>");
                    $('#searchResult').append("<br />");
                    $('#searchResult').append("<span class='search-result-info'>" + qr.snippet + "</span>");
                    $('#searchResult').append('</div>');
                    $('#searchResult').append("<br />");
                }
            }
        });
    }
});


$("#searchWorksheetButton").click(function () {
    $("#divworksheetsearchresult").html("");
    var query = document.getElementById('qw').value;  
	if (typeof(query) == "undefined" || !query) return;
	
    var url = 'https://www.googleapis.com/customsearch/v1?key=AIzaSyCMGfdDaSfjqv5zYoS0mTJnOT3e9MURWkU&cx=017800740009285026140:WMX-1508934631&q=' + query + '&siteSearch=http://www.ipracticemath.com/WorkSheets';
    //var url = 'https://www.googleapis.com/customsearch/v1?key=AIzaSyCMGfdDaSfjqv5zYoS0mTJnOT3e9MURWkU&cx=017800740009285026140:WMX-1508934631&q=' + query;
    var trackEvent = "GoogleCustomSearch";    
       //_gaq.push(['_trackEvent', trackEvent, 'clickSearch']); //Category -trackUrl, Action - clickSearch
		var loc = "'" + document.location || 'notFound' + "'";
        ga('send', 'event', 'SearchWorksheetPage', query, loc, { 'nonInteraction': 1 });

    $.ajax({
        type: "GET",
        url: url,
        crossDomain: true,
        dataType: 'jsonp',
        success: function (data) {            
            $("#divworksheetsearchresult").html("");

            $("#divworksheetsearchresult").append("<br />");

            if (data == null || data == 'undefined')
                return;

            var totalResults = 0;
            if (data.items != null && data.items.length > 0)
                totalResults = data.items.length;

            $("#divworksheetsearchresult").append("<span class='search-summary' >About " + totalResults + " results (" + data.searchInformation.searchTime.toFixed(2) + " seconds)</span>");
            $("#divworksheetsearchresult").append("<br />");

            if (totalResults == 0) {
                $("#divworksheetsearchresult").append("<br />");
                var notFound = "Your search <b>" + query + "</b>  did not match any documents." + "<br /><br />" +
                "<u>Suggestions:</u>" + "<br />" +
                "• Make sure that all words are spelled correctly." + "<br />" +
                "• Try different keywords." + "<br />" +
                "• Try more general keywords.";
                $("#divworksheetsearchresult").append("<span class='search-result-norecord'>" + notFound + "</span>");
                $("#divworksheetsearchresult").append("<br />");
            }

            if (data.items == null || data.items.length == 0)
                return;
            for (var i = 0; i <= data.items.length; i++) {
                var qr = data.items[i]; if (qr == null || qr == 'undefined')
                    continue;                
                $("#divworksheetsearchresult").append('<div id="r' + i + '" class="search-result-div">');
                $("#divworksheetsearchresult").append("<a href='" + qr.link + "' target='_blank' class='search-result-url' ><span class='search-result-title'>" + qr.title + "</span></a>");
                $("#divworksheetsearchresult").append("<br />");
                $("#divworksheetsearchresult").append("<a href='" + qr.link + "' target='_blank' class='search-result-url' >" + qr.formattedUrl + "</a>");
                $("#divworksheetsearchresult").append("<br />");
                $("#divworksheetsearchresult").append("<span class='search-result-info'>" + qr.snippet + "</span>");
                $("#divworksheetsearchresult").append('</div>');
                $("#divworksheetsearchresult").append("<br />");
                $("#hrefClose").removeClass("hide");
            }
        }
    });    
});

function CloseSearch() {
    $("#divworksheetsearchresult").html("");
    $("#hrefClose").addClass("hide");
}

//search for error page

$(document).ready(function () {
    var query = $("#hdnReturnUrl").val();
	
    //if (query != "") 
	if (typeof(query) !== "undefined" && query)
	{
        var url = 'https://www.googleapis.com/customsearch/v1?key=AIzaSyCMGfdDaSfjqv5zYoS0mTJnOT3e9MURWkU&cx=017800740009285026140:WMX-1508934631&filter=0&q=' + query;

        var trackEvent = "GoogleCustomSearch";
        //_gaq.push(['_trackEvent', trackEvent, 'clickSearch']); //Category -trackUrl, Action - clickSearch
		var loc = "'" + document.location || 'notFound' + "'";
        ga('send', 'event', 'SearchErrorPage', query, loc, { 'nonInteraction': 1 });
        //ga('send', 'event', 'textbox', 'click', 'clickSearch', { 'nonInteraction': 1 });
        //_setCustomVar(index, name, value, opt_scope)
        //index (Required) -1 to 5, name (required) - name of cusom variable, value (required) - value of customVar,
        //opt_scope —Optional - The scope for the custom variable. the scope defines the level of user engagement with your site.
        //possible values are 1 (visitor-level), 2 (session-level), or 3 (page-level). When left undefined, the custom variable scope defaults to page-level interaction.
        //_gaq.push(['_setCustomVar', 1, 'searchQuery', query, 1]);
        ga('set', '1', query);
        $.ajax({
            type: "GET",
            url: url,
            crossDomain: true,
            dataType: 'jsonp',
            success: function (data) {
                $("#divErrorPageResult").html("");

                $('#divErrorPageResult').append("<br />");

                if (data == null || data == 'undefined')
                    return;

                var totalResults = 0;
                if (data.items != null && data.items.length > 0)
                    totalResults = data.items.length;

                $('#divErrorPageResult').append("<span class='search-summary' >About " + totalResults + " results (" + data.searchInformation.searchTime.toFixed(2) + " seconds)</span>");
                $('#divErrorPageResult').append("<br />");

                if (totalResults == 0) {
                    $('#divErrorPageResult').append("<br />");
                    var notFound = "Your search <b>" + query + "</b>  did not match any documents." + "<br /><br />" +
                    "<u>Suggestions:</u>" + "<br />" +
                    "• Make sure that all words are spelled correctly." + "<br />" +
                    "• Try different keywords." + "<br />" +
                    "• Try more general keywords.";
                    $('#divErrorPageResult').append("<span class='search-result-norecord'>" + notFound + "</span>");
                    $('#divErrorPageResult').append("<br />");
                }

                if (data.items == null || data.items.length == 0)
                    return;

                for (var i = 0; i <= data.items.length; i++) {
                    var qr = data.items[i]; if (qr == null || qr == 'undefined')
                        continue;
                    $('#divErrorPageResult').append('<div id="r' + i + '" class="search-result-div">');
                    $("#divErrorPageResult").append("<a href='" + qr.link + "' target='_blank' class='search-result-url' ><span class='search-result-title'>" + qr.title + "</span></a>");
                    //$('#divErrorPageResult').append("<span class='search-result-title'>" + qr.title + "</span>");
                    $('#divErrorPageResult').append("<br />");
                    $('#divErrorPageResult').append("<a href='" + qr.link + "' class='search-result-url' >" + qr.formattedUrl + "</a>");
                    $('#divErrorPageResult').append("<br />");
                    $('#divErrorPageResult').append("<span class='search-result-info'>" + qr.snippet + "</span>");
                    $('#divErrorPageResult').append('</div>');
                    $('#divErrorPageResult').append("<br />");
                }
            }
        });
    }
});