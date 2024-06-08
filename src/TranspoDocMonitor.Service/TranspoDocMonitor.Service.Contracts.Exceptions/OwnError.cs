using TranspoDocMonitor.Service.Core.Exception;

namespace TranspoDocMonitor.Service.Contracts.Exceptions
{
    public enum OwnError
    {
        [ServiceOwnError(message: "Unable to create user")]
        UnableToCreateUser = 1,

        [ServiceOwnError(message: "Cannot find role")]
        CanNotFindRole = 2,

        [ServiceOwnError(message: "Cannot find user")]
        CanNotFindUser = 3,

        [ServiceOwnError(message: "Cannot find dictionary")]
        CanNotFindDictionary = 4,

        [ServiceOwnError(message: "Cannot create type document")]
        CanNotCreateDocumentType = 5,

        [ServiceOwnError(message: "Cannot create vehicle")]
        CanNotCreateVehicle = 6,

        [ServiceOwnError(message: "Cannot create transport document")]
        CanNotCreateTransportDocument = 7,

        [ServiceOwnError(message:"Cannot send email message")]
        CanNotSendEmailMessage = 8,

        [ServiceOwnError(message:"Cannot find transport document")]
        CanNotFindTransportDocument = 9,

        [ServiceOwnError(message:"Cannot delete user")]
        CanNotDeleteUser = 10,

        [ServiceOwnError(message: "Cannot delete vehicle")]
        CanNotDeleteVehicle = 11,
        
        [ServiceOwnError(message: "Cannot access ")]
        CanNotAccess = 11,
    }
}
