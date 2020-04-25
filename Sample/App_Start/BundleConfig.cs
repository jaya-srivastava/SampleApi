using System.Web;
using System.Web.Optimization;

namespace iPractice
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region jQuery Bundels


            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                    "~/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/scripts/jquery-1.11.2.min.js",
                        "~/scripts/jquery.validate.min.js",
                        "~/scripts/jquery.unobtrusive-ajax.min.js",
                        "~/scripts/jquery.validate.unobtrusive.min.js",
                        "~/scripts/bootstrap.min.js",
                        "~/scripts/dropmenu.js",
                        "~/scripts/html5placeholder.jquery.js",
                         "~/scripts/math.min.js",
                        "~/scripts/layoutScript.js",
                        "~/scripts/youTube.js",
                        "~/scripts/customsearch.js",
                        "~/scripts/jquery.toastmessage.js",
                        "~/scripts/sketch.js",
                        "~/scripts/pricingplan.js"
                        //moved from scripts/q bundle
                        ));

            bundles.Add(new ScriptBundle("~/scripts/canvas").Include(
                        //"~/scriptangularjs2/controller/html2canvas.min.js",
                        //"~/scriptangularjs2/controller/canvas2image.min.js",
                        //"~/scriptangularjs2/controller/base64.js"//,
                        "~/scriptangularjs2/controller/canvasimage.js"
                        ));

            bundles.Add(new ScriptBundle("~/scripts/homejs").Include(
                        "~/scripts/jquery-1.11.2.min.js",
                        //"~/scripts/jquery.validate.min.js",
                        "~/scripts/jquery.unobtrusive-ajax.min.js",
                        //"~/scripts/jquery.validate.unobtrusive.min.js",
                        "~/scripts/bootstrap.min.js",
                        "~/scripts/dropmenu.js",
                        "~/scripts/html5placeholder.jquery.js",
                        "~/scripts/layoutScript.js",
                        "~/scripts/customsearch.js"
                        //"~/scripts/ajax-util.js",
                        //"~/scripts/iPracticeScripts.js"                    
            ));

            // Extra FIles for jQuery.UI
            //Added in _layout.Cshtml for reports
            bundles.Add(new ScriptBundle("~/scripts/jqueryui").Include(
                        "~/scripts/jquery-ui.min.1.11.3.js"
                       ));

            //Contains Only on Question Layout Not need app Bundle in Question Layout 
            //bundles.Add(new ScriptBundle("~/scripts/q0").Include(
            //         "~/scripts/jquery.raty.js",
            //         //"~/scripts/jquery.toastmessage.js",
            //         "~/scripts/ajax-util.js",
            //         "~/scripts/iPracticeScripts.js",
            //         "~/scripts/knockout-2.3.0.js",
            //         "~/scripts/knockoutUtil.js",
            //         "~/scripts/timecircles.js",
            //         "~/Scripts/questionformat.js"
            //        ));

            //Include everywhere except in Question Layout
            //App if included with type="text/javascript" then shouldnt' contain ant file
            //which run the function - by self running it during load of page
            bundles.Add(new ScriptBundle("~/scripts/a").Include(
                    "~/scripts/ajax-util.js",
                    "~/scripts/iPracticeScripts.js",
                    "~/scripts/worksheet.main.js"
                // "~/Scripts/socialMedia.js"
                    ));           
           

            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                     "~/scripts/angular.min.js",
                    "~/scripts/angular-sanitize.min.js"
                    //"~/Scripts/stacktrace-0.6.4.js"
                    //,"~/Scripts/ErrorLogging.js"
                    ));

            //reports is depedent on jQuery and Angular bundle - means Anuglar and jQuery bundle is necessary before adding this bundle
             bundles.Add(new ScriptBundle("~/scripts/reports").Include(
                    "~/scriptangularjs2/controller/reportapp.js",
                    "~/scriptangularjs2/controller/chartcontroller.js",
                    "~/scriptangularjs2/controller/reportscontroller.js",
                    "~/scripts/reports.js"
                    // "~/scripts/highcharts.js"
                    ));

             bundles.Add(new ScriptBundle("~/scripts/quizold").Include(
                    "~/scriptangularjs/controller/questionservice.js",
                   "~/scriptangularjs/controller/quizcontroller.js",
                   "~/scriptangularjs/controller/quizquestioncontroller.js",
                    "~/scriptangularjs/controller/quizcompletedresult.js",
                    "~/scriptangularjs/controller/reviewquiz.js"
                   ));           

            //Question is depedent on jQuery and Angular bundle - means Anuglar and jQuery bundle is necessary before adding this bundle
            
            bundles.Add(new ScriptBundle("~/scripts/qold").Include(
                  "~/scriptangularjs/controller/angularerrorlogging.js",
                  "~/scriptangularjs/controller/questionservice.js",
                  "~/scripts/jquery.raty.js",
                  //"~/scripts/jquery.toastmessage.js",
                  "~/scripts/ajax-util.js",
                  "~/scripts/ipracticescripts.js",
                 "~/scriptangularjs/controller/questioncontroller.js",
                   "~/scripts/timecircles.js"
                   //"~/scripts/sketch.js"
                 ));
            bundles.Add(new ScriptBundle("~/scripts/qnew").Include(
                  "~/scriptangularjs2/controller/angularerrorlogging.js",
                  "~/scriptangularjs2/controller/mathquill_directive.js",
                  "~/scriptangularjs2/controller/app.js",                   
                  "~/scripts/jquery.raty.js",  
                  "~/scripts/ajax-util.js",
                  "~/scripts/ipracticescripts.js",                  
                  "~/scriptangularjs2/controller/utilityservice.js",
                  //"~/scriptangularjs2/controller/answerformat.js",                  
                  "~/scriptangularjs2/controller/answermatching.js",
                  "~/scriptangularjs2/controller/questiondirective.js",
                  "~/scriptangularjs2/controller/fabric.min.js",
                  //"~/scriptangularjs2/controller/fabricjs.js",
                  "~/scriptangularjs2/controller/fabricdirective.js",
                  "~/scriptangularjs2/controller/questionservice.js",
                  "~/scriptangularjs2/controller/questioncontroller.js",
                  "~/scriptangularjs2/controller/DbAnswersTestingController.js",
                  "~/scriptangularjs2/controller/ngdraggable.js",
                  "~/scriptangularjs2/controller/answertesting.js",                                   
                   "~/scripts/timecircles.js"
                   //"~/scripts/highcharts.js"
                //"~/scripts/sketch.js"
                 ));

            //Quiz is depedent on jQuery and Angular bundle - means Anuglar and jQuery bundle is necessary before adding this bundle
            bundles.Add(new ScriptBundle("~/scripts/quiznew").Include(
                "~/scripts/ipracticescripts.js",
                    "~/scriptangularjs2/controller/angularerrorlogging.js",
                  "~/scriptangularjs2/controller/mathquill_directive.js",
                  "~/scriptangularjs2/controller/app.js",
                    "~/scriptangularjs2/controller/utilityservice.js",
                  "~/scriptangularjs2/controller/answermatching.js",
                  "~/scriptangularjs2/controller/questiondirective.js",                
                  "~/scriptangularjs2/controller/fabric.min.js",
                  //"~/scriptangularjs2/controller/fabricjs.js",
                  "~/scriptangularjs2/controller/fabricdirective.js",
                  "~/scriptangularjs2/controller/questionservice.js",
                   "~/scriptangularjs2/controller/quizcontroller.js",
                   "~/scriptangularjs2/controller/quizquestioncontroller.js",
                    "~/scriptangularjs2/controller/quizcompletedresult.js",
                    "~/scriptangularjs2/controller/reviewquiz.js",
                    "~/scriptangularjs2/controller/ngdraggable.js"
                    //"~/scripts/highcharts.js"
                   ));

            bundles.Add(new ScriptBundle("~/scripts/katex").Include(
                        "~/scriptangularjs2/controller/katex.min.js",
                        "~/scriptangularjs2/controller/mathquill-0.10.0.js"
                        ));
            #endregion

            #region Style Budle
            bundles.UseCdn = true;

            bundles.Add(new StyleBundle("~/fonts", "http://fonts.googleapis.com/css?family=Open+Sans:300,400"));
            bundles.Add(new StyleBundle("~/content/css/katexcss").Include(
                "~/content/css/katex.min.css",
                "~/content/css/toolbar.css",
                "~/content/css/mathquill-0.10.0.css",
                 "~/content/css/ngdraggable.css"
               ));

            bundles.Add(new StyleBundle("~/content/css/stylecss").Include(
                        "~/content/css/style_new.css",  
                        "~/content/css/media.css",
                        "~/content/css/jquery.toastmessage.css",
                        "~/content/css/jquery-ui.css",
                        "~/content/css/bootstrap.css",
                        "~/content/css/bootstrap-theme.css",
                        "~/content/css/docs.css",
                         "~/content/css/used.css",  
                        //"~/content/css/prettify.css",                        
                        "~/content/css/timecircles.css",
                        "~/content/css/commonstyles.css",
                        "~/content/css/app.css",                                              
                        "~/content/css/dropmenu.css",
                        "~/content/css/icon.css"
                        //"~/content/css/learn.css",
                        //"~/content/css/question.css",
                        //"~/content/css/style2.css", //not used need to remove form svn
                        //"~/content/css/elements.css", //not used need to remove form svn
                        //"~/content/css/bootstrap-glyphicons.css",        
                        //"~/content/css/algebra.css",   //not used need to remove form svn
                        //"~/content/css/box-shadows.css",//not used need to remove form svn
                         ));
            bundles.Add(new StyleBundle("~/content/css/pricingplan").Include(
                         "~/content/css/pricing_v.css",
                         "~/content/css/page_pricing.css"
                      ));

            bundles.Add(new StyleBundle("~/content/css/homestyle").Include(
                        "~/content/css/style_new.css",
                        "~/content/css/media.css",                       
                        "~/content/css/bootstrap.css",
                        "~/content/css/bootstrap-theme.css",
                        "~/content/css/docs.css",
                        "~/content/css/used.css",                       
                        "~/content/css/commonstyles.css",
                        "~/content/css/app.css",
                        //"~/content/css/Used.css",
                        "~/content/css/dropmenu.css" 
                        //"~/content/css/elements.css",
                        //"~/content/css/icon.css" 
                        //"~/content/css/jquery.toastmessage.css",
                        // "~/content/css/box-shadows.css",   //not used need to rmeove from svn
                         ));
            #endregion

            
        }
    }
}