using constant.ConstResponse;

namespace model.ResponseModel;

public class ResponseModel{

    public string code {get;set;}
    public string message {get;set;}
    public object data {get;set;}

    public ResponseModel Success(){
        ResponseModel res = new ResponseModel{
            code = ConstResponse.SUCCESSCODE,
            message = ConstResponse.SUCCESS
        };
        return res;
    }
    public ResponseModel SuccessWithData(object data){
        ResponseModel res = new ResponseModel{
            code = ConstResponse.SUCCESSCODE,
            message = ConstResponse.SUCCESS,
            data = data
        };
        return res;
    }
}