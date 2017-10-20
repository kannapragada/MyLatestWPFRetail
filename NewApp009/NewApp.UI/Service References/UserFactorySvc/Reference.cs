﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewApp.UserFactorySvc {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserFactorySvc.IUserFactory")]
    public interface IUserFactory {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/GetUsers", ReplyAction="http://tempuri.org/IUserFactory/GetUsersResponse")]
        bool GetUsers(out string errorMessage, out NewApp.BusinessTier.Models.User[] UserObjArray, string User);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/GetUsersByCriteria", ReplyAction="http://tempuri.org/IUserFactory/GetUsersByCriteriaResponse")]
        bool GetUsersByCriteria(out string errorMessage, out NewApp.BusinessTier.Models.User[] UserObjArray, string UserId, string UserName, string Address, string IDProofTypeId, string IDProofValue, System.DateTime FromDateofBirth, System.DateTime ToDateofBirth, System.DateTime FromDateofStartRelationship, System.DateTime ToDateofStartRelationship, System.DateTime FromDateofExpiryRelationship, System.DateTime ToDateofExpiryRelationship);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/GetUserSaleDetails", ReplyAction="http://tempuri.org/IUserFactory/GetUserSaleDetailsResponse")]
        bool GetUserSaleDetails(out string errorMessage, out NewApp.BusinessTier.Models.Sale[] SaleObjArray, string UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/GetUserBasicDetails", ReplyAction="http://tempuri.org/IUserFactory/GetUserBasicDetailsResponse")]
        bool GetUserBasicDetails(out string errorMessage, out NewApp.BusinessTier.Models.User UserObj, string User);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/AddUserDetails", ReplyAction="http://tempuri.org/IUserFactory/AddUserDetailsResponse")]
        bool AddUserDetails(out string errorMessage, NewApp.BusinessTier.Models.User UserObj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/ModifyUserDetails", ReplyAction="http://tempuri.org/IUserFactory/ModifyUserDetailsResponse")]
        void ModifyUserDetails();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/DeleteUserDetails", ReplyAction="http://tempuri.org/IUserFactory/DeleteUserDetailsResponse")]
        bool DeleteUserDetails(out string errorMessage, NewApp.BusinessTier.Models.User UserObj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/AddUserPhoto", ReplyAction="http://tempuri.org/IUserFactory/AddUserPhotoResponse")]
        bool AddUserPhoto(out string errorMessage, int Userid, NewApp.BusinessTier.Models.User UserObj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/GetUserPhoto", ReplyAction="http://tempuri.org/IUserFactory/GetUserPhotoResponse")]
        bool GetUserPhoto(out byte[] imgBytes, out string errorMessage, int Userid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserFactory/AuthenticateUser", ReplyAction="http://tempuri.org/IUserFactory/AuthenticateUserResponse")]
        bool AuthenticateUser(out string errorMessage, string User, string Password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserFactoryChannel : NewApp.UserFactorySvc.IUserFactory, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserFactoryClient : System.ServiceModel.ClientBase<NewApp.UserFactorySvc.IUserFactory>, NewApp.UserFactorySvc.IUserFactory {
        
        public UserFactoryClient() {
        }
        
        public UserFactoryClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserFactoryClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserFactoryClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserFactoryClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool GetUsers(out string errorMessage, out NewApp.BusinessTier.Models.User[] UserObjArray, string User) {
            return base.Channel.GetUsers(out errorMessage, out UserObjArray, User);
        }
        
        public bool GetUsersByCriteria(out string errorMessage, out NewApp.BusinessTier.Models.User[] UserObjArray, string UserId, string UserName, string Address, string IDProofTypeId, string IDProofValue, System.DateTime FromDateofBirth, System.DateTime ToDateofBirth, System.DateTime FromDateofStartRelationship, System.DateTime ToDateofStartRelationship, System.DateTime FromDateofExpiryRelationship, System.DateTime ToDateofExpiryRelationship) {
            return base.Channel.GetUsersByCriteria(out errorMessage, out UserObjArray, UserId, UserName, Address, IDProofTypeId, IDProofValue, FromDateofBirth, ToDateofBirth, FromDateofStartRelationship, ToDateofStartRelationship, FromDateofExpiryRelationship, ToDateofExpiryRelationship);
        }
        
        public bool GetUserSaleDetails(out string errorMessage, out NewApp.BusinessTier.Models.Sale[] SaleObjArray, string UserId) {
            return base.Channel.GetUserSaleDetails(out errorMessage, out SaleObjArray, UserId);
        }
        
        public bool GetUserBasicDetails(out string errorMessage, out NewApp.BusinessTier.Models.User UserObj, string User) {
            return base.Channel.GetUserBasicDetails(out errorMessage, out UserObj, User);
        }
        
        public bool AddUserDetails(out string errorMessage, NewApp.BusinessTier.Models.User UserObj) {
            return base.Channel.AddUserDetails(out errorMessage, UserObj);
        }
        
        public void ModifyUserDetails() {
            base.Channel.ModifyUserDetails();
        }
        
        public bool DeleteUserDetails(out string errorMessage, NewApp.BusinessTier.Models.User UserObj) {
            return base.Channel.DeleteUserDetails(out errorMessage, UserObj);
        }
        
        public bool AddUserPhoto(out string errorMessage, int Userid, NewApp.BusinessTier.Models.User UserObj) {
            return base.Channel.AddUserPhoto(out errorMessage, Userid, UserObj);
        }
        
        public bool GetUserPhoto(out byte[] imgBytes, out string errorMessage, int Userid) {
            return base.Channel.GetUserPhoto(out imgBytes, out errorMessage, Userid);
        }
        
        public bool AuthenticateUser(out string errorMessage, string User, string Password) {
            return base.Channel.AuthenticateUser(out errorMessage, User, Password);
        }
    }
}
