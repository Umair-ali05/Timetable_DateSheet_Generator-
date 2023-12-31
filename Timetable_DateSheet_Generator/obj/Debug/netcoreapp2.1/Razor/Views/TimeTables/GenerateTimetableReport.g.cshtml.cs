#pragma checksum "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "292ceac11655275132d5bdd6b5e2a25ee659973f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_TimeTables_GenerateTimetableReport), @"mvc.1.0.view", @"/Views/TimeTables/GenerateTimetableReport.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/TimeTables/GenerateTimetableReport.cshtml", typeof(AspNetCore.Views_TimeTables_GenerateTimetableReport))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\_ViewImports.cshtml"
using Timetable_DateSheet_Generator;

#line default
#line hidden
#line 2 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\_ViewImports.cshtml"
using Timetable_DateSheet_Generator.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"292ceac11655275132d5bdd6b5e2a25ee659973f", @"/Views/TimeTables/GenerateTimetableReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37f9702909450ff4f5c0a7cb93dbff0b9aee7fd5", @"/Views/_ViewImports.cshtml")]
    public class Views_TimeTables_GenerateTimetableReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Timetable_DateSheet_Generator.Controllers.Administration.Report>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugins/jquery/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
  
    ViewData["Title"] = "TimetableSummary";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(184, 1173, true);
            WriteLiteral(@"


<div class=""container-fluid"">
    <div class=""row mb-2"">
        <div class=""col-sm-6"">
            <h1><i style=""color:#17a2b8"" class=""far fa-calendar-alt""></i> Timetable Report</h1>
        </div>        
    </div>
</div><!-- /.container-fluid -->
<div class=""card-body"">
    <table id=""example1"" class=""table table-bordered "">
        <thead class=""table-avatar"" style=""color:#656565;text-align:center"">
            <tr style=""font-size:12px;"">
                <th style=""font-weight:400"">#</th>
                <th style=""font-weight:400"">Offered Course (Contact Hours)</th>
                <th style=""font-weight:400"">Time(Day)</th>
                <th style=""font-weight:400"">Program Timings(H-Constraint)</th>
                <th style=""font-weight:400"">Room Timings(H-Constraint)</th>
                <th style=""font-weight:400"">Room Clash(H-Constraint)</th>
                <th style=""font-weight:400"">Class Clash(H-Constraint)</th>
                <th style=""font-weight:400"">Faculty Clash");
            WriteLiteral("(H-Constraint)</th>\r\n                <th style=\"font-weight:400\">Faculty Availibility(%)</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
            EndContext();
#line 32 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
              
                int i = 0;
            

#line default
#line hidden
            BeginContext(1416, 12, true);
            WriteLiteral("            ");
            EndContext();
#line 35 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
             foreach (var report in Model)
            {

#line default
#line hidden
            BeginContext(1475, 70, true);
            WriteLiteral("                <tr style=\"font-size:12px;\">\r\n                    <td>");
            EndContext();
            BeginContext(1547, 5, false);
#line 38 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                    Write(i + 1);

#line default
#line hidden
            EndContext();
            BeginContext(1553, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(1585, 75, false);
#line 39 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                   Write(report.OfferedCourseTimeSlots.ElementAt(0).OfferedCourse.OfferedCourseTitle);

#line default
#line hidden
            EndContext();
            BeginContext(1660, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(1664, 39, false);
#line 39 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                                                                                                  Write(report.OfferedCourseTimeSlots.Count / 4);

#line default
#line hidden
            EndContext();
            BeginContext(1704, 32, true);
            WriteLiteral(")</td>\r\n                    <td>");
            EndContext();
            BeginContext(1737, 134, false);
#line 40 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                   Write(report.OfferedCourseTimeSlots.ElementAt(0).TimeSlots.Time.getTime(report.OfferedCourseTimeSlots.ElementAt(0).TimeSlots.Time.StartTime));

#line default
#line hidden
            EndContext();
            BeginContext(1871, 4, true);
            WriteLiteral(" to ");
            EndContext();
            BeginContext(1876, 173, false);
#line 40 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                                                                                                                                                              Write(report.OfferedCourseTimeSlots.ElementAt(0).TimeSlots.Time.getTime(report.OfferedCourseTimeSlots.ElementAt(report.OfferedCourseTimeSlots.Count - 1).TimeSlots.Time.FinishTime));

#line default
#line hidden
            EndContext();
            BeginContext(2049, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(2052, 136, false);
#line 40 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                                                                                                                                                                                                                                                                                                                                              Write(report.OfferedCourseTimeSlots.ElementAt(0).TimeSlots.Time.getDay(@report.OfferedCourseTimeSlots.ElementAt(0).TimeSlots.Time.TimeWeekDay));

#line default
#line hidden
            EndContext();
            BeginContext(2188, 31, true);
            WriteLiteral(")</td>\r\n                    <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2219, "\"", 2229, 2);
            WriteAttributeValue("", 2224, "PT", 2224, 2, true);
#line 41 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
WriteAttributeValue(" ", 2226, i, 2227, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2230, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(2232, 20, false);
#line 41 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                              Write(report.ProgamTimings);

#line default
#line hidden
            EndContext();
            BeginContext(2252, 30, true);
            WriteLiteral("</td>\r\n                    <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2282, "\"", 2292, 2);
            WriteAttributeValue("", 2287, "RT", 2287, 2, true);
#line 42 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
WriteAttributeValue(" ", 2289, i, 2290, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2293, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(2295, 18, false);
#line 42 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                              Write(report.RoomTimings);

#line default
#line hidden
            EndContext();
            BeginContext(2313, 30, true);
            WriteLiteral("</td>\r\n                    <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2343, "\"", 2353, 2);
            WriteAttributeValue("", 2348, "RC", 2348, 2, true);
#line 43 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
WriteAttributeValue(" ", 2350, i, 2351, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2354, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(2356, 16, false);
#line 43 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                              Write(report.RoomClash);

#line default
#line hidden
            EndContext();
            BeginContext(2372, 30, true);
            WriteLiteral("</td>\r\n                    <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2402, "\"", 2412, 2);
            WriteAttributeValue("", 2407, "CC", 2407, 2, true);
#line 44 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
WriteAttributeValue(" ", 2409, i, 2410, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2413, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(2415, 17, false);
#line 44 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                              Write(report.ClassClash);

#line default
#line hidden
            EndContext();
            BeginContext(2432, 30, true);
            WriteLiteral("</td>\r\n                    <td");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2462, "\"", 2472, 2);
            WriteAttributeValue("", 2467, "FC", 2467, 2, true);
#line 45 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
WriteAttributeValue(" ", 2469, i, 2470, 2, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2473, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(2475, 19, false);
#line 45 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                              Write(report.FacultyClash);

#line default
#line hidden
            EndContext();
            BeginContext(2494, 31, true);
            WriteLiteral("</td>\r\n                    <td>");
            EndContext();
            BeginContext(2526, 25, false);
#line 46 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                   Write(report.FacultyAvailibilty);

#line default
#line hidden
            EndContext();
            BeginContext(2551, 30, true);
            WriteLiteral("</td>\r\n                </tr>\r\n");
            EndContext();
#line 48 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\TimeTables\GenerateTimetableReport.cshtml"
                i += 1;
            }

#line default
#line hidden
            BeginContext(2621, 791, true);
            WriteLiteral(@"        </tbody>
        <tfoot>
            <tr style=""font-size:12px;"">
                <th style=""font-weight:400"">#</th>
                <th style=""font-weight:400"">Offered Course (Contact Hours)</th>
                <th style=""font-weight:400"">Time(Day)</th>
                <th style=""font-weight:400"">Program Timings(H-Constraint)</th>
                <th style=""font-weight:400"">Room Timings(H-Constraint)</th>
                <th style=""font-weight:400"">Room Clash(H-Constraint)</th>
                <th style=""font-weight:400"">Class Clash(H-Constraint)</th>
                <th style=""font-weight:400"">Faculty Clash(H-Constraint)</th>
                <th style=""font-weight:400"">Faculty Availibility(%)</th>
            </tr>
        </tfoot>

    </table>
</div>
");
            EndContext();
            BeginContext(3412, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "418a8128cac84729a77befb08a1f1e22", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3466, 12, true);
            WriteLiteral("\r\n<script>\r\n");
            EndContext();
            BeginContext(5653, 21, true);
            WriteLiteral("    $(function () {\r\n");
            EndContext();
            BeginContext(6147, 2142, true);
            WriteLiteral(@"        //$(""#example1"").DataTable({
        //    ""responsive"": true, ""lengthChange"": false, ""autoWidth"": false,
        //    ""buttons"": [
        //        {
        //            extend: 'copy',
        //            title: 'Timetable Report',
        //            text: window.copyButtonTrans,
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
        //            }
        //        },
        //        {
        //            extend: 'csv',
        //            title: 'Timetable Report',
        //            text: window.csvButtonTrans,
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
        //            }
        //        },
        //        {
        //            extend: 'excel',
        //            title: 'Timetable Report',
        //            text: window.excelButtonTrans,
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, ");
            WriteLiteral(@"5, 6, 7,8]
        //            }
        //        },
        //        {
        //            extend: 'pdf',
        //            title: 'Timetable Report',
        //            text: window.pdfButtonTrans,
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
        //            }
        //        },
        //        {
        //            extend: 'print',
        //            title: 'Timetable Report',
        //            text: window.printButtonTrans,
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
        //            }
        //        },
        //        {
        //            extend: 'colvis',
        //            title: 'Timetable Report',
        //            text: window.colvisButtonTrans,
        //            exportOptions: {
        //                columns: [0, 1,2,3,4,5,6,7,8]
        //            }
        //        },
        //    ]
        //}");
            WriteLiteral(").buttons().container().appendTo(\'#example1_wrapper .col-md-6:eq(0)\');\r\n    });\r\n</script>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Timetable_DateSheet_Generator.Controllers.Administration.Report>> Html { get; private set; }
    }
}
#pragma warning restore 1591
