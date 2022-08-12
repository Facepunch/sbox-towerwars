using System;

namespace TowerWars.Pathfinding;

public class AStarWorld
{
	public int Width { get; }
	public int Height { get; }

	private readonly bool[] _tiles;
	private ulong _version;

	private ulong _lastSnapshotVersion;
	private AStarWorldSnapshot _lastSnapshot;

	public AStarWorld( int width, int height )
	{
		if ( width <= 1 ) throw new ArgumentOutOfRangeException( nameof( width ) );
		if ( height <= 1 ) throw new ArgumentOutOfRangeException( nameof( height ) );

		Width = width;
		Height = height;

		_tiles = new bool[width * height];
	}

	public bool this[Position position]
	{
		get => _tiles[position.Y * Width + position.X];
		set
		{
			_tiles[position.Y * Width + position.X] = value;
			_version++;
		}
	}

	public AStarWorldSnapshot GetSnapshot(bool useCache = true)
	{
		if ( useCache && _lastSnapshot != null && _version == _lastSnapshotVersion )
		{
			return _lastSnapshot;
		}

		var snapshot = new AStarWorldSnapshot( _version, Width, Height, (bool[])_tiles.Clone() );

		if ( useCache )
		{
			_lastSnapshot = snapshot;
			_lastSnapshotVersion = _version;
		}

		return snapshot;
	}
}
