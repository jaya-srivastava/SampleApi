function TextFormat(FormateType, QuestionFormatJson) {
    if (FormateType == "MultiLineFormat") {
        return MultiLineFormate(QuestionFormatJson);
    }
    var obj = JSON.parse(QuestionFormatJson);
    var isQuestionAdded = false;
    if (FormateType == "AlgebraFormat") {
        //{"Equation1":"x   - 2 = 10","Equation2":"x   +   2= 14","Equation3":"x  *   2= 24"}
        return AlgebraFormat(QuestionFormatJson);
    }
    if (FormateType == "GeoShapeFormat") {
        return GeoShapeFormat(QuestionFormatJson);
    }
}
function MultiLineFormate(QuestionFormatJson) {
    var finalFormate = '';
    finalFormate = '<div><strong>Question: ' + QuestionFormatJson;
    finalFormate = finalFormate.replace(/<br\s*[\/]?>/g, "</strong></div><div><strong style='padding-left:145px;'>");
    finalFormate = finalFormate + '</strong></div>';
    return finalFormate;
}
function AlgebraFormat(QuestionFormatJson) {
    var obj = JSON.parse(QuestionFormatJson);
    var isQuestionAdded = false;
    var finalFormate = '';
    if (obj.Equation1 != undefined) {
        finalFormate += '<div><strong>Question: </strong>' + obj.Equation1 + '</div>';
        isQuestionAdded = true;
    }
    if (obj.Equation2 != undefined) {
        finalFormate += '<div>' + (isQuestionAdded != false ? '<strong style="padding-left:145px;"> </strong>' : '<strong>Question: </strong>') + obj.Equation2 + '</div>';
        isQuestionAdded = true;
    }
    if (obj.Equation3 != undefined) {
        finalFormate += '<div>' + (isQuestionAdded != false ? '<strong style="padding-left:145px;"> </strong>' : '<strong>Question: </strong>') + obj.Equation3 + '</div>';
        isQuestionAdded = true;
    }
    return finalFormate;
}
function GeoShapeFormat(QuestionFormatJson) {

    var obj = JSON.parse(QuestionFormatJson);
    var canvas = new fabric.StaticCanvas('canvas');
    var parm = '';
    if (obj.GeoFigureType == "circle") {
        canvas.add(new fabric.Circle({ top: 100, left: 200, radius: 50, fill: 'Green', }));
        parm = 'radius = ' + obj.parameter.radius;

    }
    if (obj.GeoFigureType == "Polygon") {



    }
    else if (obj.GeoFigureType == "rectangle") {
        canvas.add(new fabric.Rect({ top: 100, left: 200, width: obj.parameter.width, height: obj.parameter.height, fill: 'Green' }));
        parm = 'width = ' + obj.parameter.width + ' , height = ' + obj.parameter.height;
    }
    else if (obj.GeoFigureType == "triangle") {
        canvas.add(new fabric.Triangle({ top: 120, left: 150, width: obj.parameter.width, height: obj.parameter.height, fill: 'Green' }));
        parm = 'width = ' + obj.parameter.width + ' , height = ' + obj.parameter.height;
    }
    return '<div><strong>Question: </strong> Find ' + obj.operationType + ' of ' + obj.GeoFigureType + ' ? (' + parm + ')</div>';;
}