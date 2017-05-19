using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Description;
using HospitaPaymentService.Wzszhjk.Utils.ConfigString;

namespace HospitaPaymentService.Wzszhjk.Common.Webservice
{
    class WebserviceOpe
    {
        public string WebserviceRetInfo(string url, string className, string operate, object[] args)
        {

            object obj = InvokeWebservice(url, @"EnterpriseServerBase.WebService.DynamicWebCalling",
                className, operate, args);

            if (null == obj)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        private object InvokeWebservice(string url, string space, string classname, string methodname, object[] args)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                System.IO.Stream stream = wc.OpenRead(url + "?WSDL");
                System.Web.Services.Description.ServiceDescription sd = System.Web.Services.Description.ServiceDescription.Read(stream);
                System.Web.Services.Description.ServiceDescriptionImporter sdi = new System.Web.Services.Description.ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                System.CodeDom.CodeNamespace cn = new System.CodeDom.CodeNamespace(space);
                System.CodeDom.CodeCompileUnit ccu = new System.CodeDom.CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);

                Microsoft.CSharp.CSharpCodeProvider csc = new Microsoft.CSharp.CSharpCodeProvider();
                System.CodeDom.Compiler.ICodeCompiler icc = csc.CreateCompiler();

                System.CodeDom.Compiler.CompilerParameters cplist = new System.CodeDom.Compiler.CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                System.CodeDom.Compiler.CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(space + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                //用来解决webservice上面有重载函数的问题
                Type[] types = new Type[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    types[i] = args[i].GetType();
                }
                System.Reflection.MethodInfo mi = t.GetMethod(methodname, types);
                if (null == mi)
                {
                    return null;
                }
                return mi.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }
    }


}
