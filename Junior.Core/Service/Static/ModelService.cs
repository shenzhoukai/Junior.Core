using Junior.Core.ServiceModel.Response;

namespace Junior.Core.Service.Static
{
    public static class ModelService
    {
        public static DataListModel MakeDataListModel(bool value, string strExtraItemName = "", string strExtraItemValue = "", string strAttItemName = "", string strAttItemValue = "")
        {
            DataListModel dataListModel = new DataListModel();
            dataListModel.Value = value;
            if (!string.IsNullOrEmpty(strExtraItemName))
                dataListModel.ExtraItemName = strExtraItemName;
            if (!string.IsNullOrEmpty(strExtraItemValue))
                dataListModel.ExtraItemValue = strExtraItemValue;
            if (!string.IsNullOrEmpty(strAttItemName))
                dataListModel.AttachedItemName = strAttItemName;
            if (!string.IsNullOrEmpty(strAttItemValue))
                dataListModel.AttachedItemValue = strAttItemValue;
            return dataListModel;
        }
        public static StatusModel MakeStatusModel(bool IsSucces, int ErrCode, string strErrMsg)
        {
            StatusModel statusModel = new StatusModel();
            statusModel.IsSuccess = IsSucces;
            statusModel.ErrCode = ErrCode;
            statusModel.ErrMsg = strErrMsg;
            return statusModel;
        }
        public static RequestModel MakeRequestModel(string strClientIP, string strClientUA, string strAbsoluteUri)
        {
            RequestModel requestModel = new RequestModel();
            requestModel.ClientIP = strClientIP;
            requestModel.ClientUA = strClientUA;
            requestModel.AbsoluteUri = strAbsoluteUri;
            return requestModel;
        }
    }
}
