#pragma checksum "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "83fcd55c6fc6ad648bfcf2529ea6a57d488dedb2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FacultyDashboard_Courses), @"mvc.1.0.view", @"/Views/FacultyDashboard/Courses.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/FacultyDashboard/Courses.cshtml", typeof(AspNetCore.Views_FacultyDashboard_Courses))]
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
#line 2 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 3 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
using Timetable_DateSheet_Generator.Data.DbContext;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"83fcd55c6fc6ad648bfcf2529ea6a57d488dedb2", @"/Views/FacultyDashboard/Courses.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37f9702909450ff4f5c0a7cb93dbff0b9aee7fd5", @"/Views/_ViewImports.cshtml")]
    public class Views_FacultyDashboard_Courses : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Timetable_DateSheet_Generator.Models.OfferedCourses>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Faculty/FacultyDashboard/View"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/dist/img/user.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(" img-circle"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("40"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("40"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugins/jquery/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 6 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
  
    ViewData["Title"] = "Offered Courses";
    Layout = "~/Views/Shared/FacultyLayout.cshtml";

#line default
#line hidden
            BeginContext(369, 515, true);
            WriteLiteral(@"<div id=""create""></div>
<div id=""edit""></div>
<div id=""delete""></div>
<div id=""view""></div>
<div id=""pass""></div>
<section class=""content-header"">   
    <div class=""container-fluid"">
        <div class=""row mb-2"">
            <div class=""col-sm-6"">
                <h1><i style=""color:#17a2b8"" class=""fa fa-book-open""></i> Offered Courses</h1>
            </div>
            <div class=""col-sm-6"">
                <ol class=""breadcrumb float-sm-right"">
                    <li class=""breadcrumb-item"">");
            EndContext();
            BeginContext(884, 55, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1c91f29f79fb4ee18405087d0049c558", async() => {
                BeginContext(926, 9, true);
                WriteLiteral("Dashboard");
                EndContext();
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
            BeginContext(939, 1747, true);
            WriteLiteral(@"</li>
                    <li class=""breadcrumb-item active"">Offered Courses</li>
                </ol>
            </div>
        </div>
    </div><!-- /.container-fluid -->
</section>
<section class=""content"">
    <div class=""container-fluid"">
        <div class=""row"">
            <div class=""col-12"">
                <!-- /.card -->
                <div class=""card"">                  
                    <!-- /.card-header -->
                    <div class=""card-body"">
                        <table id=""example1"" class=""table table-bordered "">
                            <thead class=""table-avatar"" style=""color:#656565;text-align:center"">
                                <tr>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-sort-numeric-down""></i> #</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-th-list""></i> Program</th>
                                    <th style=""font-wei");
            WriteLiteral(@"ght:400""><i style=""color:#17a2b8"" class=""fa fa-book-open""></i> Name</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-calendar-check""></i> Credit Hours</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-clock""></i> Contact Hours</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-list-ol""></i> Semester</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-check""></i> Actions</th>
                                </tr>
                            </thead>
                            <tbody>
");
            EndContext();
#line 51 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                  
                                    int i = 0;
                                

#line default
#line hidden
            BeginContext(2805, 32, true);
            WriteLiteral("                                ");
            EndContext();
#line 54 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
            BeginContext(2902, 158, true);
            WriteLiteral("                                    <tr>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(3062, 5, false);
#line 58 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                        Write(i + 1);

#line default
#line hidden
            EndContext();
            BeginContext(3068, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 59 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                              
                                                var user = Context.Users.Where(c => c.Id.Equals(item.User)).FirstOrDefault();
                                                if (user != null)
                                                {
                                                    if (string.IsNullOrEmpty(user.Image))
                                                    {

#line default
#line hidden
            BeginContext(3509, 56, true);
            WriteLiteral("                                                        ");
            EndContext();
            BeginContext(3565, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "e87e1933d17d4b3da930bbcf2071aa14", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3639, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 66 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                                    }
                                                    else
                                                    {

#line default
#line hidden
            BeginContext(3809, 60, true);
            WriteLiteral("                                                        <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 3869, "\"", 3886, 1);
#line 69 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
WriteAttributeValue("", 3875, user.Image, 3875, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3887, 46, true);
            WriteLiteral(" class=\" img-circle\" height=\"40\" width=\"40\">\r\n");
            EndContext();
#line 70 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                                    }
                                                }
                                            

#line default
#line hidden
            BeginContext(4086, 163, true);
            WriteLiteral("                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(4250, 24, false);
#line 75 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                       Write(item.Program.ProgramName);

#line default
#line hidden
            EndContext();
            BeginContext(4274, 165, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(4440, 23, false);
#line 78 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                       Write(item.OfferedCourseTitle);

#line default
#line hidden
            EndContext();
            BeginContext(4463, 165, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(4629, 29, false);
#line 81 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                       Write(item.OfferedCourseCreditHours);

#line default
#line hidden
            EndContext();
            BeginContext(4658, 165, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(4824, 30, false);
#line 84 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                       Write(item.OfferedCourseContactHours);

#line default
#line hidden
            EndContext();
            BeginContext(4854, 165, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(5020, 28, false);
#line 87 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                       Write(item.OfferedCourseSemesterNo);

#line default
#line hidden
            EndContext();
            BeginContext(5048, 4, true);
            WriteLiteral(" - (");
            EndContext();
            BeginContext(5053, 25, false);
#line 87 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                                                        Write(item.OfferedCourseSection);

#line default
#line hidden
            EndContext();
            BeginContext(5078, 225, true);
            WriteLiteral(")\r\n                                        </td>\r\n                                        <td class=\"project-actions text-right\">\r\n\r\n                                            <button class=\"btn btn-outline-secondary btn-sm\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 5303, "\"", 5390, 6);
            WriteAttributeValue("", 5313, "this.disabled", 5313, 13, true);
            WriteAttributeValue(" ", 5326, "=", 5327, 2, true);
            WriteAttributeValue(" ", 5328, "true;", 5329, 6, true);
            WriteAttributeValue(" ", 5334, "LoadDetails(", 5335, 13, true);
#line 91 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
WriteAttributeValue("", 5347, item.OfferedCourseID, 5347, 21, false);

#line default
#line hidden
            WriteAttributeValue("", 5368, ");this.disabled=false;", 5368, 22, true);
            EndWriteAttribute();
            BeginContext(5391, 373, true);
            WriteLiteral(@">
                                                <i class=""fas fa-folder"">
                                                </i>
                                                View
                                            </button>                                          
                                        </td>
                                    </tr>
");
            EndContext();
#line 98 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                                    i += 1;
                                }

#line default
#line hidden
            BeginContext(5844, 1501, true);
            WriteLiteral(@"                            </tbody>
                            <tfoot class=""table-avatar"" style=""color:#656565;text-align:center"">
                                <tr>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-sort-numeric-up""></i> #</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-th-list""></i> Program</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-book-open""></i> Name</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-calendar-check""></i> Credit Hours</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-clock""></i> Contact Hours</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-list-ol""></i> Semester</th>
                                    <th style=""font-");
            WriteLiteral(@"weight:400""><i style=""color:#17a2b8"" class=""fa fa-check""></i> Actions</th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                    <!-- /.card-body -->
                </div>
                <!-- /.card -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.container-fluid -->
</section>

<!-- jQuery -->
");
            EndContext();
            BeginContext(7345, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "daf77edd093b4080b6fdffd01fc9c015", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7399, 977, true);
            WriteLiteral(@"

<script>
   
    function LoadDetails(ID) {
        $(""#edit"").load(""/Faculty/FacultyDashboard/Details?ID="" + parseInt(ID),
            function (response, status, xhr) {
                if (status == ""success"") {
                } else if (status == ""error"" || status == ""timeout"") {
                    toastr.error('Sorry! An error has occurred. Please try again later');

                } else if (status == ""notmodified"") {
                    // NOT MODIFIED request (likely cached) //
                    toastr.error('Sorry! An error has occurred. Please try again later');

                } else if (status == ""abort"") {
                    // ABORTED request //
                    toastr.error('ABORTED request');
                }
            });
    }


    window.onload = function () {
        var el = document.getElementById(""nav_course"");
        el.classList.add(""navbar-cyan"");        
    }
    $(function () {
        if ('");
            EndContext();
            BeginContext(8377, 19, false);
#line 154 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
        Write(ViewBag.MessageType);

#line default
#line hidden
            EndContext();
            BeginContext(8396, 59, true);
            WriteLiteral("\'.toLowerCase() == \"success\")\r\n            toastr.success(\'");
            EndContext();
            BeginContext(8456, 15, false);
#line 155 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                       Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(8471, 23, true);
            WriteLiteral("\');\r\n        else if (\'");
            EndContext();
            BeginContext(8495, 19, false);
#line 156 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
             Write(ViewBag.MessageType);

#line default
#line hidden
            EndContext();
            BeginContext(8514, 53, true);
            WriteLiteral("\'.toLowerCase() == \"info\")\r\n            toastr.info(\'");
            EndContext();
            BeginContext(8568, 15, false);
#line 157 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                    Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(8583, 23, true);
            WriteLiteral("\');\r\n        else if (\'");
            EndContext();
            BeginContext(8607, 19, false);
#line 158 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
             Write(ViewBag.MessageType);

#line default
#line hidden
            EndContext();
            BeginContext(8626, 55, true);
            WriteLiteral("\'.toLowerCase() == \"error\")\r\n            toastr.error(\'");
            EndContext();
            BeginContext(8682, 15, false);
#line 159 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                     Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(8697, 23, true);
            WriteLiteral("\');\r\n        else if (\'");
            EndContext();
            BeginContext(8721, 19, false);
#line 160 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
             Write(ViewBag.MessageType);

#line default
#line hidden
            EndContext();
            BeginContext(8740, 56, true);
            WriteLiteral("\'.toLowerCase() == \"info\")\r\n            toastr.warning(\'");
            EndContext();
            BeginContext(8797, 15, false);
#line 161 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyDashboard\Courses.cshtml"
                       Write(ViewBag.Message);

#line default
#line hidden
            EndContext();
            BeginContext(8812, 2003, true);
            WriteLiteral(@"');
        else;
        $(""#example1"").DataTable({
            ""responsive"": true, ""lengthChange"": false, ""autoWidth"": false,
            ""buttons"": [
                {
                    extend: 'copy',
                    title: 'Offered Courses',
                    text: window.copyButtonTrans,
                    exportOptions: {
                        columns: [0,1,2,3,4,5]
                    }
                },
                {
                    extend: 'csv',
                    title: 'Offered Courses',
                    text: window.csvButtonTrans,
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    }
                },
                {
                    extend: 'excel',
                    title: 'Offered Courses',
                    text: window.excelButtonTrans,
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    }
                },
       ");
            WriteLiteral(@"         {
                    extend: 'pdf',
                    title: 'Offered Courses',
                    text: window.pdfButtonTrans,
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    }
                },
                {
                    extend: 'print',
                    title: 'Offered Courses',
                    text: window.printButtonTrans,
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    }
                },
                {
                    extend: 'colvis',
                    title: 'Offered Courses',
                    text: window.colvisButtonTrans,
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    }
                },
            ]
        }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    });
</script>

");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Timetable_DateSheet_Context Context { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<ApplicationUser> signInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Timetable_DateSheet_Generator.Models.OfferedCourses>> Html { get; private set; }
    }
}
#pragma warning restore 1591