﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewApp.IDProofTypeFactorySvc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="IDProofTypeFactorySvc.IIDProofTypeFactory")]
    public interface IIDProofTypeFactory {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIDProofTypeFactory/GetAllIDProofTypes", ReplyAction="http://tempuri.org/IIDProofTypeFactory/GetAllIDProofTypesResponse")]
        bool GetAllIDProofTypes(out NewApp.BusinessTier.Models.IDProofType[] IDProofTypeObjArray, out string errorMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IIDProofTypeFactory/GetIDProofType", ReplyAction="http://tempuri.org/IIDProofTypeFactory/GetIDProofTypeResponse")]
        bool GetIDProofType(out NewApp.BusinessTier.Models.IDProofType IDProofType, out string errorMessage, int IDProofTypeId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IIDProofTypeFactoryChannel : NewApp.IDProofTypeFactorySvc.IIDProofTypeFactory, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IDProofTypeFactoryClient : System.ServiceModel.ClientBase<NewApp.IDProofTypeFactorySvc.IIDProofTypeFactory>, NewApp.IDProofTypeFactorySvc.IIDProofTypeFactory {
        
        public IDProofTypeFactoryClient() {
        }
        
        public IDProofTypeFactoryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IDProofTypeFactoryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IDProofTypeFactoryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IDProofTypeFactoryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool GetAllIDProofTypes(out NewApp.BusinessTier.Models.IDProofType[] IDProofTypeObjArray, out string errorMessage) {
            return base.Channel.GetAllIDProofTypes(out IDProofTypeObjArray, out errorMessage);
        }
        
        public bool GetIDProofType(out NewApp.BusinessTier.Models.IDProofType IDProofType, out string errorMessage, int IDProofTypeId) {
            return base.Channel.GetIDProofType(out IDProofType, out errorMessage, IDProofTypeId);
        }
    }
}
