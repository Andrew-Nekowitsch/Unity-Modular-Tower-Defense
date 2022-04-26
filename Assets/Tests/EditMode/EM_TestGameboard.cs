using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EM_TestGameboard
{
    [Test]
    public void Borders()
    {
        var board = new Gameboard();
        board.width = 3;
        board.height = 3;
        board.board = new RectBoard();

        Assert.AreEqual(board.board.Tiles[0, 0].GetTileType(), TileType.Border);
        Assert.AreEqual(board.board.Tiles[0, 1].GetTileType(), TileType.Border);
        Assert.AreEqual(board.board.Tiles[0, 2].GetTileType(), TileType.Border);
        
        Assert.AreEqual(board.board.Tiles[1, 0].GetTileType(), TileType.Border);
        Assert.AreEqual(board.board.Tiles[1, 1].GetTileType(), TileType.Start);
        Assert.AreEqual(board.board.Tiles[1, 2].GetTileType(), TileType.Border);

        Assert.AreEqual(board.board.Tiles[2, 0].GetTileType(), TileType.Border);
        Assert.AreEqual(board.board.Tiles[2, 1].GetTileType(), TileType.Border);
        Assert.AreEqual(board.board.Tiles[2, 2].GetTileType(), TileType.Border);

    }
}
