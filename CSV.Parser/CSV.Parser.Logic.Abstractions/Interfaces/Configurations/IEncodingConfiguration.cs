﻿using System.Text;

namespace CSV.Parser.Logic.Abstractions.Interfaces.Configurations
{
    public interface IEncodingConfiguration
    {
        Encoding DefaultInputEncoding { get; }

        Encoding OutputEncoding { get; }
    }
}