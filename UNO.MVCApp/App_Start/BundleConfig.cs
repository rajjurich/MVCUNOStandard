using System.Web;
using System.Web.Optimization;

namespace UNO.MVCApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/js/jquery.js",
                        "~/Scripts/bootstrap.js",
                       "~/Scripts/js/bootstrap-datepicker.js",
                       "~/Scripts/jquery.multi-select.js",
                       "~/Scripts/jquery.quicksearch.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryLayout").Include(
                        "~/Scripts/js/sparkline.js",
                        "~/Scripts/js/scrollup/jquery.scrollUp.js",
                        "~/Scripts/js/alertify/alertify.js",
                        "~/Scripts/js/alertify/alertify-custom.js",
                         "~/Scripts/js/jquery.ui.js",
                         "~/Scripts/js/calendar/fullcalendar-min.js",
                        "~/Scripts/js/calendar/fullcalendar.js",
                        "~/Scripts/js/custom.js",
                        "~/Scripts/JS/jquery.dataTables.js",                                            
                         "~/Scripts/js/dataTables.bootstrap4.min.js",
                         "~/Scripts/js/dataTables.buttons.js",
                         //"~/Scripts/js/datatables.js",
                          "~/Scripts/js/custom-datatables.js",
                         "~/Scripts/js/multi.js",
                         "~/Scripts/ConfirmExit.js",//D:\Amol Data\UNO\Project\UNO.MVCApp\Scripts\JS\SpryValidationSelect.js
                         "~/Scripts/js/SpryValidationSelect.js",
                         "~/Scripts/js/jszip.js",
                         "~/Scripts/js/buttons.html5.js",
                         "~/Scripts/js/pdfmake.js",
                         "~/Scripts/js/buttons.print.js",
                         "~/Scripts/moment.js", 
                // newly added
                //"~/Scripts/js/multipleAccordion.js",
                //"~/Scripts/js/profile.js",
                //"~/Scripts/js/layout.js",
                //"~/Scripts/js/gridforms.js",
                "~/Scripts/js/jquery.stepy.js",

                 "~/Scripts/js/demo-formwizard.js",
                 "~/Scripts/JS/select2/js/select2.min.js"

                         ));
            //).ForceOrdered());





            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/js/jquery.ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Home").Include(
                "~/Scripts/js/jqueryfullPage_min.js",
                "~/Scripts/js/index.js",
                "~/Scripts/js/TMForm.js",
                "~/Scripts/js/login.js"
                ));

            //<script src='https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.js'></script>




            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.


            bundles.Add(new StyleBundle("~/Content/csshome").Include(
                  "~/Content/css/Site.css",
                "~/Content/css/bootstrap.css",
                "~/Content/css/style.css",
                "~/Content/css/style1.css",
                "~/Content/css/multi-select.dist.css",
                "~/Content/css/multi-select.dev.css",
                "~/Content/css/multi-select.css",
                "~/Content/css/jqueryfullPage.css",
                "~/Content/fonts/fontawesome.css"));

            bundles.Add(new StyleBundle("~/Content/cssLayout").Include(
                    "~/Content/css/Site.css",
                 "~/Content/css/bootstrap.css",
                "~/Content/css/animate.css",
                "~/Content/css/alertify/alertify.core.css",
                "~/Content/css/alertify/alertify.default.css",
                "~/Content/css/main.css",
                "~/Content/css/fullcalendar.css",
                 "~/Content/fonts/font-awesome.css",
                 "~/Content/css/layout.css",
                 "~/Content/css/buttons.dataTables.css",//D:\Amol Data\UNO\Project\UNO.MVCApp\Content\css\SpryValidationSelect.css
                 "~/Content/css/SpryValidationSelect.css",
                 "~/Content/css/multi.css",
                 "~/Content/css/bootstrap-datepicker.css",
                 "~/Content/select2/css/select2.min.css"
                 //"~/dataTables.bootstrap4.min.css"
                //,"~/Content/css/datepicker.css", // newly added
                //"~/Content/css/gridforms.css",
                //"~/Content/css/layout.css"

                ));


            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }

    }

}