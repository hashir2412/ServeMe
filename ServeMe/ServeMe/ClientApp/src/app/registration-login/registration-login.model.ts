export class UserModel {
    userID?: number;
    name: string;
    phone: string;
    email: string;
    receiveCommunication: boolean;
    isCustomer?: boolean;
    point?: number;
    agreement?: boolean;
    address?: string;
    totalEarnings?: number;
}

export class UserRequestModel{
    userId?: number;
    name?: string;
    phone?: string;
    email: string;
    receiveCommunication?: boolean;
    point?: number;
}

export class RegisterUserRequestModel {
    user: UserRequestModel;
    password: string;
}

export class RegisterVendorRequestModel {
    vendor: VendorRequestModel;
    password: string;
}

export class VendorRequestModel{
    vendorId?: number;
    name?: string;
    phone?: string;
    email?: string;
    receiveCommunication?: boolean;
    agreement?: boolean;
    address?: string;
    totalEarnings?: number;
}