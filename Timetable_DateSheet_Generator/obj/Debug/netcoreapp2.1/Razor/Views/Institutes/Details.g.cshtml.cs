#pragma checksum "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6be0dda462ffc303801fbebe245843ba1f8d26c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Institutes_Details), @"mvc.1.0.view", @"/Views/Institutes/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Institutes/Details.cshtml", typeof(AspNetCore.Views_Institutes_Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6be0dda462ffc303801fbebe245843ba1f8d26c0", @"/Views/Institutes/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37f9702909450ff4f5c0a7cb93dbff0b9aee7fd5", @"/Views/_ViewImports.cshtml")]
    public class Views_Institutes_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Timetable_DateSheet_Generator.Models.Institutes>
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
            BeginContext(176, 1027, true);
            WriteLiteral(@"

<div style=""display:none"" id=""Click_Details"" class=""=btn btn-primary"" data-toggle=""modal"" data-target=""#modal-lg_Details""></div>
<div class=""modal fade"" id=""modal-lg_Details"">
    <div class=""modal-dialog modal-lg"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Institute Details</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""row"">
                    <div class=""col-sm-8"">
                       
                        <div class=""row"">
                            <div class=""col-sm-12"" style=""padding-top:30%;"">
                                <h5 style=""text-align:center""><i style=""color:#17a2b8"" class=""fa fa-university""></i> Institute</h5>
                                <p style=""text-align:center;"">");
            WriteLiteral("<i>");
            EndContext();
            BeginContext(1204, 19, false);
#line 23 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
                                                            Write(Model.InstituteName);

#line default
#line hidden
            EndContext();
            BeginContext(1223, 283, true);
            WriteLiteral(@"</i></p>
                            </div>                           
                        </div>                        

                    </div>
                    <div class=""col-sm-4"">
                        <h6>Modified By</h6>
                        <small><i>");
            EndContext();
            BeginContext(1507, 54, false);
#line 30 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
                             Write(Model.Last_Modified.ToString("dd/MMM/yyy hh:mm:ss tt"));

#line default
#line hidden
            EndContext();
            BeginContext(1561, 240, true);
            WriteLiteral("</i></small>\r\n                        <div class=\"card card-cyan card-outline\" style=\"background-color:#F5F5F5\">\r\n                            <div class=\"card-body box-profile\">\r\n                                <div class=\"text-center\">\r\n\r\n");
            EndContext();
#line 35 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
                                     if (!string.IsNullOrEmpty(ViewBag.Pic))
                                    {

#line default
#line hidden
            BeginContext(1918, 90, true);
            WriteLiteral("                                        <img class=\"profile-user-img img-fluid img-circle\"");
            EndContext();
            BeginWriteAttribute("src", "\r\n                                             src=\"", 2008, "\"", 2072, 1);
#line 38 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
WriteAttributeValue("", 2060, ViewBag.Pic, 2060, 12, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2073, 76, true);
            WriteLiteral("\r\n                                             alt=\"User profile picture\">\r\n");
            EndContext();
#line 40 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
                                    }
                                    else
                                    {

#line default
#line hidden
            BeginContext(2269, 40, true);
            WriteLiteral("                                        ");
            EndContext();
            BeginContext(2309, 196, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ade0a9e4a26d4154b8cc638692817802", async() => {
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
            BeginContext(2505, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 46 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
                                    }

#line default
#line hidden
            BeginContext(2546, 113, true);
            WriteLiteral("                                </div>\r\n                                <h3 class=\"profile-username text-center\">");
            EndContext();
            BeginContext(2660, 12, false);
#line 48 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
                                                                    Write(ViewBag.Name);

#line default
#line hidden
            EndContext();
            BeginContext(2672, 395, true);
            WriteLiteral(@"</h3>
                                <p class=""text-muted text-center"">Administrator</p>
                                <ul class=""list-group list-group-unbordered mb-3"">
                                    <li class=""list-group-item"" style=""background-color:#F5F5F5"">
                                        <span class=""text-muted"">Email</span><small><i> <a href=""#"" class=""float-right"">");
            EndContext();
            BeginContext(3068, 13, false);
#line 52 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\Institutes\Details.cshtml"
                                                                                                                   Write(ViewBag.Email);

#line default
#line hidden
            EndContext();
            BeginContext(3081, 722, true);
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
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Timetable_DateSheet_Generator.Models.Institutes> Html { get; private set; }
    }
}
#pragma warning restore 1591
