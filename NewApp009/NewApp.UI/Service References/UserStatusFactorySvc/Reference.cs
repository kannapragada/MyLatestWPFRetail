﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewApp.UserStatusFactorySvc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserStatusFactorySvc.IUserStatusFactory")]
    public interface IUserStatusFactory {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserStatusFactory/GetAllUserStatus", ReplyAction="http://tempuri.org/IUserStatusFactory/GetAllUserStatusResponse")]
        bool GetAllUserStatus(out NewApp.BusinessTier.Models.UserStatus[] UserStatusObjArray, out string errorMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserStatusFactory/GetUserStatus", ReplyAction="http://tempuri.org/IUserStatusFactory/GetUserStatusResponse")]
        bool GetUserStatus(out NewApp.BusinessTier.Models.UserStatus UserStatus, out string errorMessage, int UserStatusId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserStatusFactoryChannel : NewApp.UserStatusFactorySvc.IUserStatusFactory, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserStatusFactoryClient : System.ServiceModel.ClientBase<NewApp.UserStatusFactorySvc.IUserStatusFactory>, NewApp.UserStatusFactorySvc.IUserStatusFactory {
        
        public UserStatusFactoryClient() {
        }
        
        public UserStatusFactoryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserStatusFactoryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserStatusFactoryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserStatusFactoryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool GetAllUserStatus(out NewApp.BusinessTier.Models.UserStatus[] UserStatusObjArray, out string errorMessage) {
            return base.Channel.GetAllUserStatus(out UserStatusObjArray, out errorMessage);
        }
        
        public bool GetUserStatus(out NewApp.BusinessTier.Models.UserStatus UserStatus, out string errorMessage, int UserStatusId) {
            return base.Channel.GetUserStatus(out UserStatus, out errorMessage, UserStatusId);
        }
    }
}
