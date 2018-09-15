﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///                                                                                                                                                             ///
///     MIT License                                                                                                                                             ///
///                                                                                                                                                             ///
///     Copyright (c) 2016 Raphaël Ernaelsten (@RaphErnaelsten)                                                                                                 ///
///                                                                                                                                                             ///
///     Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),      ///
///     to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute,                  ///
///     and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:              ///
///                                                                                                                                                             ///
///     The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.                          ///
///                                                                                                                                                             ///
///     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,     ///
///     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER      ///
///     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS    ///
///     IN THE SOFTWARE.                                                                                                                                        ///
///                                                                                                                                                             ///
///     PLEASE CONSIDER CREDITING AURA IN YOUR PROJECTS. IF RELEVANT, USE THE UNMODIFIED LOGO PROVIDED IN THE "LICENSE" FOLDER.                                 ///
///                                                                                                                                                             ///
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using UnityEngine;

namespace AuraAPI
{
    /// <summary>
    /// Generates all the possible variants for the ComputeDataComputeShader out of the FrustumParametersEnum enum
    /// </summary>
    [ExecuteInEditMode]
    public class GenerateComputeShaderKernelVariants : MonoBehaviour
    {
#if UNITY_EDITOR
        public string kernelName = "ComputeDataBuffer";
        public string destinationFilename = "ComputeDataComputeShader Kernel Variants";
        [Space]
        public bool generateNow;

        private string _outputString;

        private void Update()
        {
            if(generateNow)
            {
                int enumLength = Enum.GetNames(typeof(FrustumParametersEnum)).Length;
                enumLength -= 1; //Because of the EnableNothing = 0

                int maxIteration = 0;
                for(int i = 0; i < enumLength; ++i)
                {
                    maxIteration += 1 << i;
                }

                _outputString = "";
                for(int i = 0; i <= maxIteration; ++i)
                {
                    string tmpString = ((FrustumParametersEnum)i).ToString();

                    _outputString += "// Kernel " + i + "\n";

                    tmpString = tmpString.Replace(",", "");
                    tmpString = tmpString.InsertStringBeforeUpperCaseLetters("_");
                    tmpString = tmpString.ToUpperInvariant();

                    _outputString += "#pragma kernel " + kernelName + " " + tmpString + "\n";
                }

                string filepath = Application.dataPath + "\\" + destinationFilename + ".txt";

                if(File.Exists(filepath))
                {
                    File.Delete(filepath);
                }

                File.WriteAllText(filepath, _outputString);

                generateNow = false;
            }
        }
#endif
    }
}