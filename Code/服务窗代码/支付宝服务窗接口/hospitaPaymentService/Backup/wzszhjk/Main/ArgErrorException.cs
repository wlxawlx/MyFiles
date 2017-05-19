using System;

namespace HospitaPaymentService
{
	/// <summary>
	/// 参数异常
	/// </summary>
	public class ArgErrorException : Exception
	{
		private string m_argname;

		public string argname {
			get {
				return m_argname;
			}
		}

		public ArgErrorException (string argname)
		{
			this.m_argname = argname;
		}
	}
}

