using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sandbox;

namespace TowerWars.Pathfinding;

public class AStarWorldSnapshot
{
	public ulong Version { get; }
	public int Width { get; }
	public int Height { get; }

	private readonly bool[] _tiles;

	public AStarWorldSnapshot( ulong version, int width, int height, bool[] tiles )
	{
		if ( tiles == null ) throw new ArgumentNullException( nameof( tiles ) );
		if ( tiles.Length != width * height ) throw new ArgumentException( "Length of tiles does not match dimensions", nameof( tiles ) );

		Version = version;
		Width = width;
		Height = height;
		_tiles = tiles;
	}

	/*public Task<AStarPath> FindPath( Position start, Position end )
	{
		return GameTask.RunInThreadAsync( async () => FindPathImpl( start, end ) );
	}*/

	public AStarPath? FindPath( Position start, Position end )
	{
		if ( start.X < 0 || start.X >= Width || start.Y < 0 || start.Y >= Height )
		{
			throw new ArgumentException( "Start is out of bounds" );
		}
		if ( end.X < 0 || end.X >= Width || end.Y < 0 || end.Y >= Height )
		{
			throw new ArgumentException( "End is out of bounds" );
		}

		var startIdx = ToIndex( start );
		var endIdx = ToIndex( end );

		var nodes = ArrayPool<Node>.Shared.Rent( _tiles.Length );
		Array.Clear( nodes );

		Node GetNode( int index ) => nodes[index] ??= new Node { Index = index };

		var closed = ArrayPool<bool>.Shared.Rent( _tiles.Length );
		Array.Clear( closed );

		var open = new List<Node>(); // sort by priority descending
		open.Add( GetNode( startIdx ) );

		while ( open.Count > 0 )
		{
			var lastIndex = open.Count - 1;
			var current = open[lastIndex];
			open.RemoveAt( lastIndex );
			closed[current.Index] = true;

			if ( current.Index == endIdx )
			{
				var path = new List<Position>();

				var node = current;
				while ( node != null )
				{
					path.Add( FromIndex( node.Index ) );

					if ( path.Count > _tiles.Length )
					{
						Log.Error( "Pathfinding code is broken!" );
						return null;
					}

					node = node.Parent;
				}

				path.Reverse();

				ArrayPool<Node>.Shared.Return( nodes );
				ArrayPool<bool>.Shared.Return( closed );

				return new AStarPath( Version, path );
			}

			var currentPos = FromIndex( current.Index );
			//CheckNeighbor( new Position( currentPos.X + 1, currentPos.Y + 1 ) );
			CheckNeighbor( new Position( currentPos.X + 1, currentPos.Y + 0 ) );
			CheckNeighbor( new Position( currentPos.X + 0, currentPos.Y + 1 ) );
			//CheckNeighbor( new Position( currentPos.X + 1, currentPos.Y - 1 ) );

			//CheckNeighbor( new Position( currentPos.X - 1, currentPos.Y - 1 ) );
			CheckNeighbor( new Position( currentPos.X - 1, currentPos.Y - 0 ) );
			CheckNeighbor( new Position( currentPos.X - 0, currentPos.Y - 1 ) );
			//CheckNeighbor( new Position( currentPos.X - 1, currentPos.Y + 1 ) );

			void CheckNeighbor( Position position )
			{
				if ( position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height )
				{
					return;
				}

				var idx = ToIndex( position );
				if ( closed[idx] || _tiles[idx] )
				{
					return;
				}

				var next = GetNode( idx );
				var newG = current.G;
				if ( next.Parent == null ) // never set any of these values yet
				{
					next.Parent = current;
					next.G = newG;
					next.H = Position.Distance( FromIndex( next.Index ), end );
				}
				else if ( newG < next.G )
				{
					next.Parent = current;
					next.G = newG;
					// H is unchanged
				}

				var index = open.BinarySearch( next );
				if ( index < 0 )
				{
					open.Insert( ~index, next );
				}
			}
		}

		ArrayPool<Node>.Shared.Return( nodes );
		ArrayPool<bool>.Shared.Return( closed );
		return null;
	}

	private int ToIndex( in Position position )
	{
		return position.Y * Width + position.X;
	}

	private Position FromIndex( int index )
	{
		var (y, x) = Math.DivRem( index, Width );
		return new Position( x, y );
	}

	private class Node : IComparable<Node>
	{
		public int Index;
		public Node Parent;
		public float G = 0;
		public float H = 0;
		public float F => G + H;

		public int CompareTo( Node other )
		{
			if ( ReferenceEquals( this, other ) )
			{
				return 0;
			}

			return -(F.CompareTo( other.F )); // descending by F
		}
	}
}
