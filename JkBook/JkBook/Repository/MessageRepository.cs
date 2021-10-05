using JkBook.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JkBook.Repository
{
    public class MessageRepository : IMessageRepository
    {
        //private readonly NewBookAlertConfig _newBookAlertConfig;

        private  NewBookAlertConfig _newBookAlertConfig;
        public MessageRepository(IOptionsMonitor<NewBookAlertConfig> newBookAlertConfig)
        {
            //_newBookAlertConfig = newBookAlertConfig.Value;
            _newBookAlertConfig = newBookAlertConfig.CurrentValue;
            newBookAlertConfig.OnChange(config =>
            {
                _newBookAlertConfig = config;
            });
        }

        public string GetName()
        {
            return _newBookAlertConfig.BookMessage;
        }
    }
}
