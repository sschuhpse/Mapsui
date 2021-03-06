﻿using System.Drawing;
using System.IO;
using Mapsui.Tests.Common;
using NUnit.Framework;
#if OPENTK
using Mapsui.Rendering.OpenTK;
#elif GDI
using Mapsui.Rendering.Gdi;
#elif MONOGAME
using Mapsui.Rendering.MonoGame;
using Mapsui.Rendering.MonoGame.Tests_W8;
#endif

namespace Mapsui.Rendering.Xaml.Tests
{
    [TestFixture, RequiresSTA]
    class MapRendererTests
    {
        [Test]
        public void RenderPointsWithVectorStyle()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithVectorStyle();
            const string fileName = "vector_symbol.png";
            
            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);
            
            // aside
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Test]
        public void RenderPointWithBitmapSymbols()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithBitmapSymbols();
            const string fileName = "points_with_symbolstyle.png";
            
            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);
            
            // aside
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Test]
        public void RenderRotatedBitmapSymbolWithOffset()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithBitmapRotatedAndOffset();
            const string fileName = "bitmap_symbol.png";

            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);

            // aside
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Test]
        public void RenderPointsWithDifferentSymbolTypes()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithDifferentSymbolTypes();
            const string fileName = "vector_symbol_symboltype.png";
            
            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);

            // aside
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Test]
        public void RenderSymbolWithWorldUnits()
        {
            // arrange
            var map = ArrangeRenderingTests.PointsWithWorldUnits();
            const string fileName = "vector_symbol_unittype.png";
            
            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);

            // aside
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Test]
        public void RenderPolygon()
        {
            // arrange
            var map = ArrangeRenderingTests.Polygon();
            const string fileName = "polygon.png";

            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);

            // aside
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Test]
        public void RenderLine()
        {
            // arrange
            var map = ArrangeRenderingTests.Line();
            const string fileName = "line.png";
            
            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);

            // aside
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Ignore]
        [Test]
        public void RenderTiles()
        {
            // arrange
            var map = ArrangeRenderingTests.Tiles();
            const string fileName = "tilelayer.png";

            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);

            // aside;
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }

        [Test]
        public void RenderLabels()
        {
            // arrange
            var map = ArrangeRenderingTests.Labels();
            const string fileName = "labels.png";

            // act
            var bitmap = new MapRenderer().RenderToBitmapStream(map.Viewport, map.Layers);

            // aside;
            File.WriteToGeneratedFolder(fileName, bitmap);

            // assert
            Assert.IsTrue(CompareBitmaps(File.ReadFromOriginalFolder(fileName), bitmap));
        }
        
        private bool CompareBitmaps(Stream bitmapStream1, Stream bitmapStream2)
        {
            bitmapStream1.Position = 0;
            bitmapStream2.Position = 0;

            var bitmap1 = (Bitmap)Image.FromStream(bitmapStream1);
            var bitmap2 = (Bitmap)Image.FromStream(bitmapStream2);

            for (var x = 0; x < bitmap1.Width; x++)
            {
                for (var y = 0; y < bitmap1.Height; y++)
                {
                    if (bitmap1.GetPixel(x, y) != bitmap2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
