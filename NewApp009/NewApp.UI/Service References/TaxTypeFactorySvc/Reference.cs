﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewApp.TaxTypeFactorySvc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TaxTypeFactorySvc.ITaxTypeFactory")]
    public interface ITaxTypeFactory {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaxTypeFactory/GetAllTaxTypes", ReplyAction="http://tempuri.org/ITaxTypeFactory/GetAllTaxTypesResponse")]
        bool GetAllTaxTypes(out NewApp.BusinessTier.Models.TaxType[] TaxTypeObjArray, out string errorMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaxTypeFactory/GetTaxType", ReplyAction="http://tempuri.org/ITaxTypeFactory/GetTaxTypeResponse")]
        bool GetTaxType(out NewApp.BusinessTier.Models.TaxType TaxType, out string errorMessage, int TaxTypeId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITaxTypeFactoryChannel : NewApp.TaxTypeFactorySvc.ITaxTypeFactory, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TaxTypeFactoryClient : System.ServiceModel.ClientBase<NewApp.TaxTypeFactorySvc.ITaxTypeFactory>, NewApp.TaxTypeFactorySvc.ITaxTypeFactory {
        
        public TaxTypeFactoryClient() {
        }
        
        public TaxTypeFactoryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TaxTypeFactoryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaxTypeFactoryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TaxTypeFactoryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool GetAllTaxTypes(out NewApp.BusinessTier.Models.TaxType[] TaxTypeObjArray, out string errorMessage) {
            return base.Channel.GetAllTaxTypes(out TaxTypeObjArray, out errorMessage);
        }
        
        public bool GetTaxType(out NewApp.BusinessTier.Models.TaxType TaxType, out string errorMessage, int TaxTypeId) {
            return base.Channel.GetTaxType(out TaxType, out errorMessage, TaxTypeId);
        }
    }
}