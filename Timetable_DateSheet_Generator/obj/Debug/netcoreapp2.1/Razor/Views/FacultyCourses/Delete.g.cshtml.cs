#pragma checksum "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fcf3835befa45cddcb35052fd04f64508834d59f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FacultyCourses_Delete), @"mvc.1.0.view", @"/Views/FacultyCourses/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/FacultyCourses/Delete.cshtml", typeof(AspNetCore.Views_FacultyCourses_Delete))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fcf3835befa45cddcb35052fd04f64508834d59f", @"/Views/FacultyCourses/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37f9702909450ff4f5c0a7cb93dbff0b9aee7fd5", @"/Views/_ViewImports.cshtml")]
    public class Views_FacultyCourses_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Timetable_DateSheet_Generator.Models.Attendance>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(176, 575, true);
            WriteLiteral(@"

<div style=""display:none"" id=""Click_Delete"" class=""=btn btn-primary"" data-toggle=""modal"" data-target=""#modal-lg_Delete""></div>
<div class=""modal fade"" id=""modal-lg_Delete"">
    <div class=""modal-dialog modal-lg"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Delete Attendance</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            ");
            EndContext();
            BeginContext(751, 1380, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e2ca8b043fb54495bc079132d6837aa6", async() => {
                BeginContext(791, 64, true);
                WriteLiteral("\r\n                <div class=\"modal-body\">\r\n                    ");
                EndContext();
                BeginContext(855, 46, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e94e69a743c049158f48467df0d7d031", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 18 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.AttendanceID);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(901, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(923, 49, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "67efaf5a3ca94c2e867cbd67556858aa", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 19 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.OfferedCourseID);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(972, 228, true);
                WriteLiteral("\r\n                    <div class=\"input-group mb-3\">\r\n                        <div class=\"col-sm-12\">\r\n                            <p>\r\n                                Are you sure to delete \r\n                                <i>");
                EndContext();
                BeginContext(1201, 38, false);
#line 24 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
                              Write(Model.OfferedCourse.OfferedCourseTitle);

#line default
#line hidden
                EndContext();
                BeginContext(1239, 2, true);
                WriteLiteral(" (");
                EndContext();
                BeginContext(1242, 52, false);
#line 24 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
                                                                       Write(Model.AttendanceDate.ToString("hh/MMM/yyy hh:ss tt"));

#line default
#line hidden
                EndContext();
                BeginContext(1294, 4, true);
                WriteLiteral(") - ");
                EndContext();
                BeginContext(1299, 47, false);
#line 24 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
                                                                                                                                Write(Attendance.getName(Model.AttendanceCreditHours));

#line default
#line hidden
                EndContext();
                BeginContext(1346, 124, true);
                WriteLiteral(" hours attendance</i>\r\n                                ?.All the information related to\r\n                                <i>");
                EndContext();
                BeginContext(1471, 38, false);
#line 26 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
                              Write(Model.OfferedCourse.OfferedCourseTitle);

#line default
#line hidden
                EndContext();
                BeginContext(1509, 2, true);
                WriteLiteral(" (");
                EndContext();
                BeginContext(1512, 52, false);
#line 26 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
                                                                       Write(Model.AttendanceDate.ToString("hh/MMM/yyy hh:ss tt"));

#line default
#line hidden
                EndContext();
                BeginContext(1564, 4, true);
                WriteLiteral(") - ");
                EndContext();
                BeginContext(1569, 47, false);
#line 26 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\FacultyCourses\Delete.cshtml"
                                                                                                                                Write(Attendance.getName(Model.AttendanceCreditHours));

#line default
#line hidden
                EndContext();
                BeginContext(1616, 508, true);
                WriteLiteral(@" hours attendance</i>
                                will be deleted.
                            </p>
                        </div>
                    </div>
                </div>
                <div class=""modal-footer justify-content-between"">
                    <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
                    <button type=""submit"" class=""btn btn-danger""><i class=""fa fa-trash""></i> Delete</button>
                </div>
            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2131, 230, true);
            WriteLiteral("\r\n        </div>\r\n        <!-- /.modal-content -->\r\n    </div>\r\n    <!-- /.modal-dialog -->\r\n</div>\r\n<script>\r\n    $(document).ready(function () {\r\n\r\n        document.getElementById(\'Click_Delete\').click();\r\n\r\n    });\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Timetable_DateSheet_Generator.Models.Attendance> Html { get; private set; }
    }
}
#pragma warning restore 1591
