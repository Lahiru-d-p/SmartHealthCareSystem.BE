using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCareSystem.Application.DTOs
{
    class ResponseDto
    {
    }
	public class ResponseModel<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }

		public ResponseModel(bool success, string message, T data)
		{
			Success = success;
			Message = message;
			Data = data;
		}
	}

}
