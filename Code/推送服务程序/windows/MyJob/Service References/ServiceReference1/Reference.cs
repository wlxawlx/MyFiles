﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NipicTask.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.HPServiceSoap")]
    public interface HPServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/accountService", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string accountService(string optype, string paramxml);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HPServiceSoapChannel : NipicTask.ServiceReference1.HPServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HPServiceSoapClient : System.ServiceModel.ClientBase<NipicTask.ServiceReference1.HPServiceSoap>, NipicTask.ServiceReference1.HPServiceSoap {
        
        public HPServiceSoapClient() {
        }
        
        public HPServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HPServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HPServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HPServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string accountService(string optype, string paramxml) {
            return base.Channel.accountService(optype, paramxml);
        }
    }
}
