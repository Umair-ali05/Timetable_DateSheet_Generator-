#pragma checksum "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a9e97f8b265cf348f6e47bf4380aa454751cd101"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_OfferedCourses_Details), @"mvc.1.0.view", @"/Views/OfferedCourses/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/OfferedCourses/Details.cshtml", typeof(AspNetCore.Views_OfferedCourses_Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9e97f8b265cf348f6e47bf4380aa454751cd101", @"/Views/OfferedCourses/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37f9702909450ff4f5c0a7cb93dbff0b9aee7fd5", @"/Views/_ViewImports.cshtml")]
    public class Views_OfferedCourses_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Timetable_DateSheet_Generator.Models.OfferedCourses>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("profile-user-img img-fluid img-circle"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/dist/img/user.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("User profile picture"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(180, 1011, true);
            WriteLiteral(@"

<div style=""display:none"" id=""Click_Details"" class=""=btn btn-primary"" data-toggle=""modal"" data-target=""#modal-lg_Details""></div>
<div class=""modal fade"" id=""modal-lg_Details"">
    <div class=""modal-dialog modal-lg"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Offered Course Details</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""row"">
                    <div class=""col-sm-8"">
                        <div class=""row"">
                            <div class=""col-sm-12"" style=""padding-top:5%;"">
                                <h5 style=""text-align:center""><i style=""color:#17a2b8"" class=""fa fa-graduation-cap""></i> Offered In</h5>
                                <p style=""text-align:center;""><i>");
            EndContext();
            BeginContext(1192, 25, false);
#line 22 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                                            Write(Model.Program.ProgramName);

#line default
#line hidden
            EndContext();
            BeginContext(1217, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(1221, 39, false);
#line 22 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                                                                         Write(Model.Program.Department.DepartmentName);

#line default
#line hidden
            EndContext();
            BeginContext(1260, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(1263, 48, false);
#line 22 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                                                                                                                   Write(Model.Program.Department.Institute.InstituteName);

#line default
#line hidden
            EndContext();
            BeginContext(1311, 322, true);
            WriteLiteral(@")</i></p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-sm-6"" >
                                <h5 ><i style=""color:#17a2b8"" class=""fa fa-university""></i> Institute</h5>
                                <p ><i>");
            EndContext();
            BeginContext(1634, 40, false);
#line 28 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Model.Department.Institute.InstituteName);

#line default
#line hidden
            EndContext();
            BeginContext(1674, 243, true);
            WriteLiteral("</i></p>\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <h5 ><i style=\"color:#17a2b8\" class=\"fa fa-th-list\"></i> Department</h5>\r\n                                <p ><i>");
            EndContext();
            BeginContext(1918, 31, false);
#line 32 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Model.Department.DepartmentName);

#line default
#line hidden
            EndContext();
            BeginContext(1949, 316, true);
            WriteLiteral(@"</i></p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-sm-6"">
                                <h5 ><i style=""color:#17a2b8"" class=""fa fa-list-ol""></i> Semester</h5>
                                <p ><i>");
            EndContext();
            BeginContext(2266, 30, false);
#line 38 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Model.Semester.getSemesterType);

#line default
#line hidden
            EndContext();
            BeginContext(2296, 3, true);
            WriteLiteral(" - ");
            EndContext();
            BeginContext(2300, 27, false);
#line 38 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                                                    Write(Model.Semester.SemesterYear);

#line default
#line hidden
            EndContext();
            BeginContext(2327, 251, true);
            WriteLiteral("</i></p>\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <h5 ><i style=\"color:#17a2b8\" class=\"fa fa-chalkboard-teacher\"></i> Faculty</h5>\r\n                                <p ><i>");
            EndContext();
            BeginContext(2579, 13, false);
#line 42 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(ViewBag.fName);

#line default
#line hidden
            EndContext();
            BeginContext(2592, 323, true);
            WriteLiteral(@"</i></p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-sm-6"">
                                <h5 ><i style=""color:#17a2b8"" class=""fa fa-book-open""></i> Course Title</h5>
                                <p ><i> ");
            EndContext();
            BeginContext(2916, 24, false);
#line 48 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                   Write(Model.OfferedCourseTitle);

#line default
#line hidden
            EndContext();
            BeginContext(2940, 252, true);
            WriteLiteral("</i></p>\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <h5 ><i style=\"color:#17a2b8\" class=\"fa fa-calendar-check\"></i> Credit Hours</h5>\r\n                                <p ><i>");
            EndContext();
            BeginContext(3193, 30, false);
#line 52 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Model.OfferedCourseCreditHours);

#line default
#line hidden
            EndContext();
            BeginContext(3223, 320, true);
            WriteLiteral(@"</i></p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-sm-6"">
                                <h5 ><i style=""color:#17a2b8"" class=""fa fa-clock""></i> Contact Hours</h5>
                                <p ><i> ");
            EndContext();
            BeginContext(3544, 24, false);
#line 58 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                   Write(Model.OfferedCourseTitle);

#line default
#line hidden
            EndContext();
            BeginContext(3568, 242, true);
            WriteLiteral("</i></p>\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <h5 ><i style=\"color:#17a2b8\" class=\"fa fa-list-alt\"></i> Category</h5>\r\n                                <p ><i>");
            EndContext();
            BeginContext(3811, 87, false);
#line 62 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Enum.GetName(typeof(OfferedCourses.CourseCategoryOptions), Model.OfferedCourseCategory));

#line default
#line hidden
            EndContext();
            BeginContext(3898, 317, true);
            WriteLiteral(@"</i></p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-sm-6"">
                                <h5 ><i style=""color:#17a2b8"" class=""fa fa-check""></i> Course Type</h5>
                                <p ><i>");
            EndContext();
            BeginContext(4216, 82, false);
#line 68 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Enum.GetName(typeof(OfferedCourses.CourseTypeOptions), Model.OfferedCourseSpecial));

#line default
#line hidden
            EndContext();
            BeginContext(4298, 253, true);
            WriteLiteral("</i></p>\r\n                            </div>\r\n                            <div class=\"col-sm-6\">\r\n                                <h5 ><i style=\"color:#17a2b8\" class=\"fa fa-sort-alpha-up\"></i> Course Section</h5>\r\n                                <p ><i>");
            EndContext();
            BeginContext(4552, 26, false);
#line 72 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Model.OfferedCourseSection);

#line default
#line hidden
            EndContext();
            BeginContext(4578, 328, true);
            WriteLiteral(@"</i></p>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-sm-6"">
                                <h5 ><i style=""color:#17a2b8"" class=""fa fa-sort-numeric-down""></i> Semester #</h5>
                                <p ><i>");
            EndContext();
            BeginContext(4907, 29, false);
#line 78 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                  Write(Model.OfferedCourseSemesterNo);

#line default
#line hidden
            EndContext();
            BeginContext(4936, 232, true);
            WriteLiteral("</i></p>\r\n                            </div>\r\n                        </div>\r\n\r\n                    </div>\r\n                    <div class=\"col-sm-4\">\r\n                        <h6>Modified By</h6>\r\n                        <small><i>");
            EndContext();
            BeginContext(5169, 54, false);
#line 85 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                             Write(Model.Last_Modified.ToString("dd/MMM/yyy hh:mm:ss tt"));

#line default
#line hidden
            EndContext();
            BeginContext(5223, 240, true);
            WriteLiteral("</i></small>\r\n                        <div class=\"card card-cyan card-outline\" style=\"background-color:#F5F5F5\">\r\n                            <div class=\"card-body box-profile\">\r\n                                <div class=\"text-center\">\r\n\r\n");
            EndContext();
#line 90 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                     if (!string.IsNullOrEmpty(ViewBag.Pic))
                                    {

#line default
#line hidden
            BeginContext(5580, 90, true);
            WriteLiteral("                                        <img class=\"profile-user-img img-fluid img-circle\"");
            EndContext();
            BeginWriteAttribute("src", "\r\n                                             src=\"", 5670, "\"", 5734, 1);
#line 93 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
WriteAttributeValue("", 5722, ViewBag.Pic, 5722, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5735, 76, true);
            WriteLiteral("\r\n                                             alt=\"User profile picture\">\r\n");
            EndContext();
#line 95 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
            BeginContext(5931, 40, true);
            WriteLiteral("                                        ");
            EndContext();
            BeginContext(5971, 196, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "c8243c2dd3ae4a85b1978ddfeaa43d6c", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6167, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 101 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                    }

#line default
#line hidden
            BeginContext(6208, 113, true);
            WriteLiteral("                                </div>\r\n                                <h3 class=\"profile-username text-center\">");
            EndContext();
            BeginContext(6322, 12, false);
#line 103 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                                                    Write(ViewBag.Name);

#line default
#line hidden
            EndContext();
            BeginContext(6334, 395, true);
            WriteLiteral(@"</h3>
                                <p class=""text-muted text-center"">Administrator</p>
                                <ul class=""list-group list-group-unbordered mb-3"">
                                    <li class=""list-group-item"" style=""background-color:#F5F5F5"">
                                        <span class=""text-muted"">Email</span><small><i> <a href=""#"" class=""float-right"">");
            EndContext();
            BeginContext(6730, 13, false);
#line 107 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\OfferedCourses\Details.cshtml"
                                                                                                                   Write(ViewBag.Email);

#line default
#line hidden
            EndContext();
            BeginContext(6743, 702, true);
            WriteLiteral(@"</a></i></small>
                                    </li>
                                </ul>
                            </div>
                            <!-- /.card-body -->
                        </div>
                    </div>
                </div>
            </div>
            <div class=""modal-footer justify-content-between"">
                <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<script>
    $(document).ready(function () {

        document.getElementById('Click_Details').click();

    });
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Timetable_DateSheet_Generator.Models.OfferedCourses> Html { get; private set; }
    }
}
#pragma warning restore 1591
