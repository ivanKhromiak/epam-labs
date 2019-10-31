// <copyright file="Rectangle.cs" company="Roman Moravskyi">
// Copyright (c) Roman Moravskyi. All rights reserved.
// </copyright>

namespace Epam.HomeWork.Lab6.Task1
{
    using System;
    using Epam.HomeWork.Lab6.Common;

    /// <summary>
    /// Rectangle with coordinates, width and height
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// Backfield for <see cref="Width" />
        /// </summary>
        private float width;

        /// <summary>
        /// Backfield for <see cref="Height" />
        /// </summary>
        private float height;

        /// <summary>
        /// Backfield for <see cref="BottomLeft" />
        /// </summary>
        private PointF bottomLeft;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> class.
        /// </summary>
        public Rectangle()
            : this(0.0f, 0.0f, PointF.Origin)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle" /> class.
        /// </summary>
        /// <param name="width">Width of rectangle</param>
        /// <param name="height">Height of rectangle</param>
        /// <param name="bottomLeft">BottomLeft <see cref="PointF" /> of rectangle</param>
        public Rectangle(float width, float height, PointF bottomLeft)
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentException("Width or height cannot be less than zero.");
            }

            this.bottomLeft = bottomLeft.Clone() as PointF;

            this.width = width;
            this.height = height;

            this.BottomRight = new PointF(
                this.bottomLeft.X + width,
                this.bottomLeft.Y);

            this.TopLeft = new PointF(
                this.bottomLeft.X,
                this.bottomLeft.Y + height);

            this.TopRight = new PointF(
                this.bottomLeft.X + width,
                this.bottomLeft.Y + height);        
        }

        /// <summary>
        /// Gets or sets width of current rectangle.
        /// </summary>
        public float Width
        {
            get => this.width;
            set
            {
                if (value < 0.0f)
                {
                    throw new ArgumentException("Width cannot be less than zero.");
                }

                this.width = value;
                this.RecalculatePoints();
            }
        }

        /// <summary>
        /// Gets or sets height of current rectangle.
        /// </summary>
        public float Height
        {
            get => this.height;
            set
            {
                if (value < 0.0f)
                {
                    throw new ArgumentException("Width cannot be less than zero.");
                }

                this.height = value;
                this.RecalculatePoints();
            }
        }

        /// <summary>
        /// Gets or sets <see cref="BottomLeft" /> of current rectangle.
        /// </summary>
        public PointF BottomLeft
        {
            get => this.bottomLeft;
            set
            {
                this.bottomLeft = value;
                this.RecalculatePoints();
            }
        }

        /// <summary>
        /// Gets <see cref="BottomRight" /> of current rectangle.
        /// </summary>
        public PointF BottomRight { get; private set; }

        /// <summary>
        /// Gets <see cref="TopLeft" /> of current rectangle.
        /// </summary>
        public PointF TopLeft { get; private set; }

        /// <summary>
        /// Gets <see cref="TopRight" /> of current rectangle.
        /// </summary>
        public PointF TopRight { get; private set; }

        /// <summary>
        /// Moves current rectangle to a new BottomLeft <see cref="PointF" /> .
        /// </summary>
        /// <param name="newBottomLeft">A new BottomLeft <see cref="PointF" /></param>
        /// <returns><see cref="Rectangle" /> </returns>
        public Rectangle MoveTo(PointF newBottomLeft)
        {
            this.BottomLeft.X = newBottomLeft.X;
            this.BottomLeft.Y = newBottomLeft.Y;
            this.RecalculatePoints();

            return this;
        }

        /// <summary>
        /// Scales current rectangle and returns it.
        /// </summary>
        /// <param name="cfX">Scaling coefficient for X</param>
        /// <param name="cfY">Scaling coefficient for Y</param>
        /// <returns><see cref="Rectangle" /></returns>
        public Rectangle Scale(float cfX, float cfY)
        {
            this.Width *= cfX;
            this.Height *= cfY;
            this.RecalculatePoints();

            return this;
        }

        /// <summary>
        /// Scales current rectangle and returns it.
        /// </summary>
        /// <param name="cf">Scaling coefficient for Y and X</param>
        /// <returns><see cref="Rectangle" /></returns>
        public Rectangle Scale(float cf)
        {
            return this.Scale(cf, cf);
        }

        /// <summary>
        /// Scales current rectangle and returns it.
        /// </summary>
        /// <param name="cf">Scaling coefficient for X</param>
        /// <returns><see cref="Rectangle" /></returns>
        public Rectangle ScaleX(float cf)
        {
            return this.Scale(cf, 1.0f);
        }

        /// <summary>
        /// Scales current rectangle and returns it.
        /// </summary>
        /// <param name="cf">Scaling coefficient for Y</param>
        /// <returns><see cref="Rectangle" /></returns>
        public Rectangle ScaleY(float cf)
        {
            return this.Scale(1.0f, cf);
        }

        /// <summary>
        /// Returns two rectangle intersect.
        /// </summary>
        /// <param name="other">Rectangle to intersect with</param>
        /// <returns><see cref="Rectangle" /></returns>
        public Rectangle Intersect(Rectangle other)
        {
            // two rectangles do not overlap
            if (this.BottomLeft.X > other.BottomRight.X
                || other.BottomLeft.X > this.BottomRight.X)
            {
                return null;
            }
            if (this.BottomLeft.Y > other.TopLeft.Y
                || other.BottomLeft.Y > this.TopLeft.Y)
            {
                return null;
            }

            float leftX = MathF.Max(this.BottomLeft.X, other.BottomLeft.X);
            float rightX = MathF.Min(this.BottomRight.X, other.BottomRight.X);
            float bottomY = MathF.Max(this.BottomLeft.Y, other.BottomLeft.Y);
            float topY = MathF.Min(this.TopLeft.Y, other.TopLeft.Y);

            return new Rectangle(
                rightX - leftX,
                topY - bottomY,
                new PointF(leftX, bottomY));
        }

        /// <summary>
        /// Recalculates points of rectangle after BottomLeft or Height/Width changes.
        /// </summary>
        private void RecalculatePoints()
        {
            this.BottomRight.Y = this.BottomLeft.Y;
            this.BottomRight.X = this.BottomLeft.X + this.Width;

            this.TopLeft.X = this.BottomLeft.X;
            this.TopLeft.Y = this.BottomLeft.Y + this.Height;

            this.TopRight.X = this.BottomRight.X;
            this.TopRight.Y = this.TopLeft.Y;
        }
    }
}
