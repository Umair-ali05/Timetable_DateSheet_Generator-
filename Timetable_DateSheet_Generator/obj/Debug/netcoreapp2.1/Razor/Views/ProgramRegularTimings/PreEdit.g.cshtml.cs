#pragma checksum "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\ProgramRegularTimings\PreEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7925c105820ee2754a2c0c310b1cf5303b36b67e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ProgramRegularTimings_PreEdit), @"mvc.1.0.view", @"/Views/ProgramRegularTimings/PreEdit.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ProgramRegularTimings/PreEdit.cshtml", typeof(AspNetCore.Views_ProgramRegularTimings_PreEdit))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7925c105820ee2754a2c0c310b1cf5303b36b67e", @"/Views/ProgramRegularTimings/PreEdit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"37f9702909450ff4f5c0a7cb93dbff0b9aee7fd5", @"/Views/_ViewImports.cshtml")]
    public class Views_ProgramRegularTimings_PreEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Timetable_DateSheet_Generator.Models.Programs>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(174, 1269, true);
            WriteLiteral(@"

<div style=""display:none"" id=""Click_Edit"" class=""=btn btn-primary"" data-toggle=""modal"" data-target=""#modal-lg_Edit""></div>
<div class=""modal fade"" id=""modal-lg_Edit"">
    <div class=""modal-dialog modal-xl"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"">Update Program Record</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
          <div id=""load_Edit_Timings"" class=""col-sm-12"">

          </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>
<script>
   
    $(document).ready(function () {
        document.getElementById('Click_Edit').click();
        var ls = document.getElementById(""load_Edit_Timings"");
        ls.innerHTML = """";
        $('#load_Edit_Timings').append(
            $('<div>').prop({
              ");
            WriteLiteral("  id: \'loaders\',\r\n                innerHTML: \'\',\r\n                className: \'loader\'\r\n            })\r\n        );\r\n        ls.innerHTML += \' Please Wait..\'\r\n        $(\"#load_Edit_Timings\").load(\"/Administration/ProgramRegularTimings/Edit?ID=\" + ");
            EndContext();
            BeginContext(1444, 15, false);
#line 38 "e:\pro\Timetable_DateSheet_Generator\Timetable_DateSheet_Generator\Views\ProgramRegularTimings\PreEdit.cshtml"
                                                                                   Write(Model.ProgramID);

#line default
#line hidden
            EndContext();
            BeginContext(1459, 867, true);
            WriteLiteral(@",
            function (response, status, xhr) {
                document.getElementById('crtBtn').disabled = false;
                if (status == ""success"") {
                    var el = document.getElementById(""loaders"");
                    el.style.display = ""none"";
                } else if (status == ""error"" || status == ""timeout"") {
                    toastr.error('Sorry! An error has occurred. Please try again later');

                } else if (status == ""notmodified"") {
                    // NOT MODIFIED request (likely cached) //
                    toastr.error('Sorry! An error has occurred. Please try again later');

                } else if (status == ""abort"") {
                    // ABORTED request //
                    toastr.error('ABORTED request');
                }
            }
        );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Timetable_DateSheet_Generator.Models.Programs> Html { get; private set; }
    }
}
#pragma warning restore 1591
