#pragma checksum "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "98f00886135273027fc209bc5ea4e6b5bf21ff23"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RegisteredCourses_LoadStudents), @"mvc.1.0.view", @"/Views/RegisteredCourses/LoadStudents.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/RegisteredCourses/LoadStudents.cshtml", typeof(AspNetCore.Views_RegisteredCourses_LoadStudents))]
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
#line 2 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 3 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
using Timetable_DateSheet_Generator.Data.DbContext;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"98f00886135273027fc209bc5ea4e6b5bf21ff23", @"/Views/RegisteredCourses/LoadStudents.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37f9702909450ff4f5c0a7cb93dbff0b9aee7fd5", @"/Views/_ViewImports.cshtml")]
    public class Views_RegisteredCourses_LoadStudents : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Timetable_DateSheet_Generator.Models.ViewModels.RegisterCourseViewModelList>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/dist/img/user.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(" img-circle"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("20"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("20"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "checkbox", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/plugins/jquery/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(276, 1672, true);
            WriteLiteral(@"<section class=""content"">
    <div class=""container-fluid"">        
        <div class=""row"">
            <div class=""col-12"">
                <!-- /.card -->
                <div class=""card"">
                    <div class=""card-header"">
                        <h3 class=""card-title"">List of students are shown below:</h3>
                    </div>
                    <!-- /.card-header -->
                    <div class=""card-body"">


                        <table id=""example1"" class=""table table-bordered "" style=""font-size:12px;"">
                            <thead class=""table-avatar"" style=""color:#656565;text-align:center"">
                                <tr>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-sort-numeric-down""></i> #</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-th-list""></i> Program</th>
                                    <th style=""font-weight:400""><i s");
            WriteLiteral(@"tyle=""color:#17a2b8"" class=""fa fa-user-graduate""></i> Name</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-sort-alpha-up""></i> Enrollment</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-envelope""></i> Email</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-check""></i> Select All <input id=""first_ch"" type=""checkbox"" onchange=""change('first_ch')"" /></th>
                                </tr>
                            </thead>
                            <tbody>
");
            EndContext();
#line 31 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                  
                                    int i = 0;
                                

#line default
#line hidden
            BeginContext(2067, 32, true);
            WriteLiteral("                                ");
            EndContext();
#line 34 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                 foreach (var item in Model.studentCourseDetailViewModel)
                                {

#line default
#line hidden
            BeginContext(2193, 160, true);
            WriteLiteral("                                    <tr>\r\n\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(2353, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "208014926c72463e9e58caacaf014187", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 39 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
AddHtmlAttributeValue("", 2364, i, 2364, 2, false);

#line default
#line hidden
            AddHtmlAttributeValue(" ", 2366, "course", 2367, 7, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 39 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.course);

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
            BeginContext(2414, 46, true);
            WriteLiteral("\r\n                                            ");
            EndContext();
            BeginContext(2460, 66, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7e60e9bbf8ad4456bd05b359ed9d965f", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 40 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
AddHtmlAttributeValue("", 2471, i, 2471, 2, false);

#line default
#line hidden
            AddHtmlAttributeValue(" ", 2473, "student", 2474, 8, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 40 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.studentId);

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
            BeginContext(2526, 46, true);
            WriteLiteral("\r\n                                            ");
            EndContext();
            BeginContext(2574, 5, false);
#line 41 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                        Write(i + 1);

#line default
#line hidden
            EndContext();
            BeginContext(2580, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 42 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                              
                                                var user = Context.Users.Where(c => c.Id.Equals(item.User)).FirstOrDefault();
                                                if (user != null)
                                                {
                                                    if (string.IsNullOrEmpty(user.Image))
                                                    {

#line default
#line hidden
            BeginContext(3021, 56, true);
            WriteLiteral("                                                        ");
            EndContext();
            BeginContext(3077, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "90f77d5e3de04cb580cfe38ccb3055ff", async() => {
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
            BeginContext(3151, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 49 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                                    }
                                                    else
                                                    {

#line default
#line hidden
            BeginContext(3321, 60, true);
            WriteLiteral("                                                        <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 3381, "\"", 3398, 1);
#line 52 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
WriteAttributeValue("", 3387, user.Image, 3387, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3399, 46, true);
            WriteLiteral(" class=\" img-circle\" height=\"20\" width=\"20\">\r\n");
            EndContext();
#line 53 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                                    }
                                                }
                                            

#line default
#line hidden
            BeginContext(3598, 163, true);
            WriteLiteral("                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(3762, 16, false);
#line 58 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                       Write(item.programName);

#line default
#line hidden
            EndContext();
            BeginContext(3778, 121, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n");
            EndContext();
#line 61 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                             if (string.IsNullOrEmpty(item.path))
                                            {

#line default
#line hidden
            BeginContext(4029, 48, true);
            WriteLiteral("                                                ");
            EndContext();
            BeginContext(4077, 74, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4b6d48f80a134e5dadb1950941ea59e4", async() => {
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
            BeginContext(4151, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 64 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                            }
                                            else
                                            {

#line default
#line hidden
            BeginContext(4297, 52, true);
            WriteLiteral("                                                <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 4349, "\"", 4365, 1);
#line 67 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
WriteAttributeValue("", 4355, item.path, 4355, 10, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(4366, 46, true);
            WriteLiteral(" class=\" img-circle\" height=\"20\" width=\"20\">\r\n");
            EndContext();
#line 68 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                            }

#line default
#line hidden
            BeginContext(4459, 44, true);
            WriteLiteral("                                            ");
            EndContext();
            BeginContext(4504, 9, false);
#line 69 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                       Write(item.Name);

#line default
#line hidden
            EndContext();
            BeginContext(4513, 165, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            ");
            EndContext();
            BeginContext(4679, 15, false);
#line 72 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                       Write(item.enrollment);

#line default
#line hidden
            EndContext();
            BeginContext(4694, 180, true);
            WriteLiteral("\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            <i><a href=\"#\">");
            EndContext();
            BeginContext(4875, 10, false);
#line 75 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                                      Write(item.Email);

#line default
#line hidden
            EndContext();
            BeginContext(4885, 241, true);
            WriteLiteral("</a></i>\r\n                                        </td>\r\n                                        <td style=\"text-align:center\">\r\n                                            <div class=\"mb-3\">\r\n                                                ");
            EndContext();
            BeginContext(5126, 63, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "218dd3fff61b45f09a99cf76b7cd101a", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#line 79 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item.isMarked);

#line default
#line hidden
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#line 79 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
AddHtmlAttributeValue("", 5178, i, 5178, 2, false);

#line default
#line hidden
            AddHtmlAttributeValue(" ", 5180, "mark", 5181, 5, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5189, 144, true);
            WriteLiteral("\r\n                                            </div>\r\n                                        </td>\r\n                                    </tr>\r\n");
            EndContext();
#line 83 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                                    i += 1;
                                }

#line default
#line hidden
            BeginContext(5413, 1557, true);
            WriteLiteral(@"                            </tbody>
                            <tfoot class=""table-avatar"" style=""color:#656565;text-align:center"">
                                <tr>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-sort-numeric-up""></i> #</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-th-list""></i> Program</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-user-graduate""></i> Name</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-sort-alpha-up""></i> Enrollment</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-envelope""></i> Email</th>
                                    <th style=""font-weight:400""><i style=""color:#17a2b8"" class=""fa fa-check""></i> Select All <input id=""second_ch"" type=""checkbox"" onchange=""change('second");
            WriteLiteral(@"_ch')"" /></th>
                                </tr>
                            </tfoot>
                        </table><hr />

                        <button class=""btn btn-info"" onclick=""Save()""><i class=""fa fa-plus""></i> Register Students</button>

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
");
            EndContext();
            BeginContext(6970, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "446c98f70c224b94b92a519ec571f3ac", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(7024, 62, true);
            WriteLiteral("\r\n<script>\r\n    function Save() {\r\n        for (var i = 0; i <");
            EndContext();
            BeginContext(7087, 40, false);
#line 114 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                      Write(Model.studentCourseDetailViewModel.Count);

#line default
#line hidden
            EndContext();
            BeginContext(7127, 1135, true);
            WriteLiteral(@"; i++)
        {
            var course = i + "" course"";
            var student = i + "" student"";
            var mark = i + "" mark"";
            var mk = document.getElementById(mark);
            if (mk.checked) {
                var st = document.getElementById(student);
                var cr = document.getElementById(course);
                $.ajax({
                    type: ""POST"",
                    url: ""/Administration/RegisteredCourses/Add?Course="" + cr.value + ""&Student="" + st.value,
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (Data) {
                    },
                    error: function () { }
                });
                toastr.success(""Students Registered Succesfully."");
            }
        }
        window.location.href = 'Actions'
    }
    function change(from) {
        var from = document.getElementById(from);
        var first_ch = document.getEleme");
            WriteLiteral("ntById(\"first_ch\");\r\n        var second_ch = document.getElementById(\"second_ch\");\r\n        for (var i = 0; i <");
            EndContext();
            BeginContext(8263, 40, false);
#line 141 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\RegisteredCourses\LoadStudents.cshtml"
                      Write(Model.studentCourseDetailViewModel.Count);

#line default
#line hidden
            EndContext();
            BeginContext(8303, 191, true);
            WriteLiteral("; i++)\r\n            document.getElementById(i + \" mark\").checked = from.checked;\r\n        first_ch.checked = from.checked;\r\n        second_ch.checked = from.checked;\r\n\r\n    }      \r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Timetable_DateSheet_Generator.Models.ViewModels.RegisterCourseViewModelList> Html { get; private set; }
    }
}
#pragma warning restore 1591