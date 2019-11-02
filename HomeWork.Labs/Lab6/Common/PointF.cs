// <copyright file="PointF.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab6.Common
{
    using System;

    /// <summary>
    /// Defines a float point in coordinates
    /// </summary>
    public class PointF : ICloneable
    {
        /// <summary>
        /// Initializes static members of the <see cref="PointF" /> class.
        /// </summary>
        static PointF()
        {
            Origin = new PointF(0.0f, 0.0f);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointF" /> class.
        /// </summary>
        public PointF()
            : this(0.0f, 0.0f)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointF" /> class.
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public PointF(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        
        /// <summary>
        /// Gets origin of coordinates
        /// </summary>
        public static PointF Origin { get; }

        /// <summary>
        /// Gets or sets X position
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets Y position
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Clones an instance of <see cref="PointF" />
        /// </summary>
        /// <returns><see cref="PointF" /></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"({this.X}; {this.Y})";
        }
    }
}
