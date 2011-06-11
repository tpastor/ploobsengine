using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Events
{
    /// <summary>
    /// Coloca um objeto em uma mensagem e envia para quem tiver interessado
    /// Content eh o conteudo (objeto acima mencionado)
    /// </summary>
    public interface IEvent<Content>
    {
        void FireEvent(Content data);
        String Code { get; set; }
    }
    
}
