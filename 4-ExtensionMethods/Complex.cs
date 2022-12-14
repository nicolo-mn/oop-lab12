namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex
    {
        private readonly double re;
        private readonly double im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real
        {
            get => re;
        }

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary
        {
            get => im;
        }

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(Real * Real + Imaginary * Imaginary);

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase => Math.Atan2(Imaginary, Real);

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            // TODO improve
            if (Imaginary == 0.0) return Real.ToString();
            var imAbs = Math.Abs(Imaginary);
            var imValue = imAbs == 1.0 ? "" : imAbs.ToString();
            string sign;
            if (Real == 0d)
            {
                sign = Imaginary > 0 ? "" : "-";
                return sign + "i" + imValue;
            }

            sign = Imaginary > 0 ? "+" : "-";
            return $"{Real} {sign} i{imValue}";
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IComplex other) => Real == other.Real && Imaginary == other.Imaginary;

        /// <inheritdoc cref="object.Equals(object?)"/>
        public override bool Equals(object obj)
        {
            // TODO improve
            if (obj is Complex)
            {
                return Equals(obj as Complex);
            }
            return false;
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            // TODO improve
            return HashCode.Combine(Real, Imaginary);
        }
    }
}
