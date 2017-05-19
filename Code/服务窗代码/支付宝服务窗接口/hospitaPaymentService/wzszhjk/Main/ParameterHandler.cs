using System;
using System.Collections.Generic;

namespace HospitaPaymentService
{
	/// <summary>
	/// 请求参数处理类
	/// </summary>
	public class ParameterHandler
	{
		private Dictionary<String, String> m_params = null;

		public Dictionary<String, String> getParams ()
		{
			return m_params;
		}

		public ParameterHandler (Dictionary<String, String> args)
		{
			this.m_params = args;
		}

		/// <summary>
		/// Gets the string.
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="key">Key.</param>
		public string getString (string key)
		{
			if (m_params.ContainsKey (key)) {
				return m_params [key];
			}

			return "";
		}

		/// <summary>
		/// Gets the not null string. 如果不存在抛出异常
		/// </summary>
		/// <returns>The not null string.</returns>
		/// <param name="key">Key.</param>
		public string getNotNullString (string key)
		{
			string result = null;
			if (m_params.ContainsKey (key)) {
				result = m_params [key];
			}

			if (string.IsNullOrEmpty (result)) {
				throw new ArgErrorException (key);
			}

			return result;
		}
	}
}

