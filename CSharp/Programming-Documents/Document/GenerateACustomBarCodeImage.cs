﻿using System.IO;
using Aspose.Words;
using System;
using Aspose.Words.Fields;
using System.Drawing;
using Aspose.BarCode;

namespace Aspose.Words.Examples.CSharp.Programming_Documents.Working_With_Document
{
   
    class GenerateACustomBarCodeImage
    {
        public static void Run()
        {
            // ExStart:GenerateACustomBarCodeImage
            // The path to the documents directory.
            string dataDir = RunExamples.GetDataDir_WorkingWithDocument();
            Document doc = new Document(dataDir + @"Document.doc");

            // Set custom barcode generator
            doc.FieldOptions.BarcodeGenerator = new CustomBarcodeGenerator();
            doc.Save(dataDir + @"GenerateACustomBarCodeImage_out.pdf");
            // ExEnd:GenerateACustomBarCodeImage
        }
    }

    // ExStart:GenerateACustomBarCodeImage_IBarcodeGenerator
    public class CustomBarcodeGenerator : IBarcodeGenerator
    {
        /// <summary>
        /// Converts barcode type from Word to Aspose.BarCode.
        /// </summary>
        /// <param name="inputCode"></param>
        /// <returns></returns>
        private static Symbology ConvertBarcodeType(string inputCode)
        {
            if (inputCode == null)
                return (Symbology)int.MinValue;

            string type = inputCode.ToUpper();

            switch (type)
            {
                case "QR":
                    return Symbology.QR;
                case "CODE128":
                    return Symbology.Code128;
                case "CODE39":
                    return Symbology.Code39Standard;
                case "EAN8":
                    return Symbology.EAN8;
                case "EAN13":
                    return Symbology.EAN13;
                case "UPCA":
                    return Symbology.UPCA;
                case "UPCE":
                    return Symbology.UPCE;
                case "ITF14":
                    return Symbology.ITF14;
                case "CASE":
                    break;
            }

            return (Symbology)int.MinValue;
        }

        /// <summary>
        /// Converts barcode image height from Word units to Aspose.BarCode units.
        /// </summary>
        /// <param name="heightInTwipsString"></param>
        /// <returns></returns>
        private static float ConvertSymbolHeight(string heightInTwipsString)
        {
            // Input value is in 1/1440 inches (twips)
            int heightInTwips = int.MinValue;
            int.TryParse(heightInTwipsString, out heightInTwips);

            if (heightInTwips == int.MinValue)
                throw new Exception("Error! Incorrect height - " + heightInTwipsString + ".");

            // Convert to mm
            return (float)(heightInTwips * 25.4 / 1440);
        }

        /// <summary>
        /// Converts barcode image color from Word to Aspose.BarCode.
        /// </summary>
        /// <param name="inputColor"></param>
        /// <returns></returns>
        private static Color ConvertColor(string inputColor)
        {
            // Input should be from "0x000000" to "0xFFFFFF"
            int color = int.MinValue;
            int.TryParse(inputColor.Replace("0x", ""), out color);

            if (color == int.MinValue)
                throw new Exception("Error! Incorrect color - " + inputColor + ".");

            return Color.FromArgb(color >> 16, (color & 0xFF00) >> 8, color & 0xFF);

            // Backword conversion -
            //return string.Format("0x{0,6:X6}", mControl.ForeColor.ToArgb() & 0xFFFFFF);
        }

        /// <summary>
        /// Converts bar code scaling factor from percents to float.
        /// </summary>
        /// <param name="scalingFactor"></param>
        /// <returns></returns>
        private static float ConvertScalingFactor(string scalingFactor)
        {
            bool isParsed = false;
            int percents = int.MinValue;
            int.TryParse(scalingFactor, out percents);

            if (percents != int.MinValue)
            {
                if (percents >= 10 && percents <= 10000)
                    isParsed = true;
            }

            if (!isParsed)
                throw new Exception("Error! Incorrect scaling factor - " + scalingFactor + ".");

            return percents / 100.0f;
        }

        /// <summary>
        /// Implementation of the GetBarCodeImage() method for IBarCodeGenerator interface.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public Image GetBarcodeImage(BarcodeParameters parameters)
        {
            if (parameters.BarcodeType == null || parameters.BarcodeValue == null)
                return null;

            BarCodeBuilder builder = new BarCodeBuilder();

            builder.SymbologyType = ConvertBarcodeType(parameters.BarcodeType);
            if (builder.SymbologyType == (Symbology)int.MinValue)
                return null;

            builder.CodeText = parameters.BarcodeValue;

            if (builder.SymbologyType == Symbology.QR)
                builder.Display2DText = parameters.BarcodeValue;

            if (parameters.ForegroundColor != null)
                builder.ForeColor = ConvertColor(parameters.ForegroundColor);

            if (parameters.BackgroundColor != null)
                builder.BackColor = ConvertColor(parameters.BackgroundColor);

            if (parameters.SymbolHeight != null)
            {
                builder.ImageHeight = ConvertSymbolHeight(parameters.SymbolHeight);
                builder.AutoSize = false;
            }

            builder.CodeLocation = CodeLocation.None;

            if (parameters.DisplayText)
                builder.CodeLocation = CodeLocation.Below;

            builder.CaptionAbove.Text = "";

            const float scale = 0.4f; // Empiric scaling factor for converting Word barcode to Aspose.BarCode
            float xdim = 1.0f;

            if (builder.SymbologyType == Symbology.QR)
            {
                builder.AutoSize = false;
                builder.ImageWidth *= scale;
                builder.ImageHeight = builder.ImageWidth;
                xdim = builder.ImageHeight / 25;
                builder.xDimension = builder.yDimension = xdim;
            }

            if (parameters.ScalingFactor != null)
            {
                float scalingFactor = ConvertScalingFactor(parameters.ScalingFactor);
                builder.ImageHeight *= scalingFactor;
                if (builder.SymbologyType == Symbology.QR)
                {
                    builder.ImageWidth = builder.ImageHeight;
                    builder.xDimension = builder.yDimension = xdim * scalingFactor;
                }

                builder.AutoSize = false;
            }
            return builder.BarCodeImage;
        }

        Image IBarcodeGenerator.GetBarcodeImage(BarcodeParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Image GetOldBarcodeImage(BarcodeParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
    // ExEnd:GenerateACustomBarCodeImage_IBarcodeGenerator
}
