using System;

namespace Common.Dependencies.Error
{
	public interface IErrorReportingDependency
	{
		void ReportError(Exception ex);
	}
}

