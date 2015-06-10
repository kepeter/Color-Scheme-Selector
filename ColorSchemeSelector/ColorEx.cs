using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorSchemeExtension
{
	public class ColorEx
	{
		private bool _IsRGBDirty = false;
		private bool _IsHSVDirty = false;

		public ColorEx ( )
		{
			RGB2HSV( );
		}

		#region Properties

		private Color _Color = new Color( )
		{
			A = 255,
			R = 128,
			G = 128,
			B = 128
		};

		public Color Color
		{
			get
			{
				if ( _IsRGBDirty )
				{
					HSV2RGB( );
				}

				return ( _Color );
			}
		}

		public byte R
		{
			get
			{
				if ( _IsRGBDirty )
				{
					HSV2RGB( );
				}

				return ( _Color.R );
			}
			set
			{
				_Color.R = value;

				_IsHSVDirty = true;
			}
		}

		public byte G
		{
			get
			{
				if ( _IsRGBDirty )
				{
					HSV2RGB( );
				}

				return ( _Color.G );
			}
			set
			{
				_Color.G = value;

				_IsHSVDirty = true;
			}
		}

		public byte B
		{
			get
			{
				if ( _IsRGBDirty )
				{
					HSV2RGB( );
				}

				return ( _Color.B );
			}
			set
			{
				_Color.B = value;

				_IsHSVDirty = true;
			}
		}

		private short _H = 0;

		public short H
		{
			get
			{
				if ( _IsHSVDirty )
				{
					RGB2HSV( );
				}

				return ( _H );
			}
			set
			{
				if ( value >= 0 && value <= 360 )
				{
					_H = value;

					_IsRGBDirty = true;
				}
			}
		}

		private byte _S = 0;

		public byte S
		{
			get
			{
				if ( _IsHSVDirty )
				{
					RGB2HSV( );
				}

				return ( _S );
			}
			set
			{
				if ( value >= 0 && value <= 100 )
				{
					_S = value;

					_IsRGBDirty = true;
				}
			}
		}

		private byte _V = 0;

		public byte V
		{
			get
			{
				if ( _IsHSVDirty )
				{
					RGB2HSV( );
				}

				return ( _V );
			}
			set
			{
				if ( value >= 0 && value <= 100 )
				{
					_V = value;

					_IsRGBDirty = true;
				}
			}
		}

		#endregion

		#region Helpers

		private void RGB2HSV ( )
		{
			double nR = _Color.R / ( double )255;
			double nG = _Color.G / ( double )255;
			double nB = _Color.B / ( double )255;
			double nCmax = Math.Max( nR, Math.Max( nG, nB ) );
			double nCmin = Math.Min( nR, Math.Min( nG, nB ) );
			double nDelta = nCmax - nCmin;
			double nH = 0;
			double nS = 0;
			double nV = nCmax;

			if ( nDelta != 0 )
			{
				if ( nCmax == nR )
				{
					nH = ( ( nG - nB ) / nDelta ) % 6.0;
				}
				else if ( nCmax == nG )
				{
					nH = ( ( nB - nR ) / nDelta ) + 2.0;
				}
				else if ( nCmax == nB )
				{
					nH = ( ( nR - nG ) / nDelta ) + 4.0;
				}
			}

			nH *= 60.0;

			if ( nH < 0 )
			{
				nH += 360;
			}

			if ( nDelta != 0 )
			{
				nS = nDelta / nCmax;
			}

			nS *= ( double )100;
			nV *= ( double )100;

			_H = Convert.ToInt16( nH );
			_S = Convert.ToByte( nS );
			_V = Convert.ToByte( nV );

			_IsHSVDirty = false;
		}

		private void HSV2RGB ( )
		{
			double nS = _S / ( double )100;
			double nV = _V / ( double )100;
			double nDelta = nV * nS;
			double nH = _H / 60.0;
			double nX = nDelta * ( 1 - Math.Abs( ( nH % 2 ) - 1 ) );
			double nM = nV - nDelta;
			double nR = 0;
			double nG = 0;
			double nB = 0;

			if ( nH >= 0 && nH < 1 )
			{
				nR = nDelta;
				nG = nX;
			}
			else if ( nH >= 1 && nH < 2 )
			{
				nR = nX;
				nG = nDelta;
			}
			else if ( nH >= 2 && nH < 3 )
			{
				nG = nDelta;
				nB = nX;
			}
			else if ( nH >= 3 && nH < 4 )
			{
				nG = nX;
				nB = nDelta;
			}
			else if ( nH >= 4 && nH < 5 )
			{
				nR = nX;
				nB = nDelta;
			}
			else
			{
				nR = nDelta;
				nB = nX;
			}

			nR += nM;
			nG += nM;
			nB += nM;

			nR *= 255;
			nG *= 255;
			nB *= 266;

			_Color.R = Convert.ToByte( nR );
			_Color.G = Convert.ToByte( nG );
			_Color.B = Convert.ToByte( nB );

			_IsRGBDirty = false;
		}

		#endregion
	}
}
