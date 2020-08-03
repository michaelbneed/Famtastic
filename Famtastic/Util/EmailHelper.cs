using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.IdentityModel.Protocols;

namespace Famtastic.Util
{
	public class EmailHelper
	{
		public static void SendSubscriberMessage(string emailAddresses, string message)
		{
			if (emailAddresses.Contains(";"))
			{
				emailAddresses = emailAddresses.Replace(";", ",");
			}

			var emails = emailAddresses.Split(",");

			foreach (var item in emails)
			{
				var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
				var emailClean = regex.IsMatch(emailAddresses);

				if (emailClean)
				{
					var smtpServerUrl = "mail.famtastic.us";
					var smtpServerPort = "25";
					var password = "S@dness1sN0tR3@l";

					var smtp = new SmtpClient(smtpServerUrl);
					smtp.Port = Convert.ToInt32(smtpServerPort, CultureInfo.InvariantCulture);

					MailAddress emailFrom = new MailAddress("admin@famtastic.us");
					smtp.Credentials = new NetworkCredential(emailFrom.Address, password);
					smtp.EnableSsl = false;

					var emailMessage = new MailMessage
					{
						From = emailFrom,
						Subject = message.Substring(0, 10),
						To = { item },
						Body = message
					};

					try
					{
						smtp.Send(emailMessage);
					}
					catch (Exception e)
					{
						continue;
					}
				}
			}
		}
	}
}
