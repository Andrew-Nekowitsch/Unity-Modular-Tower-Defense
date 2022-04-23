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
        board.board = new RectBoard(1,1);

        Assert.AreEqual(board.board.Tiles[0, 0].Type, TileType.Border);
        Assert.AreEqual(board.board.Tiles[0, 1].Type, TileType.Border);
        Assert.AreEqual(board.board.Tiles[0, 2].Type, TileType.Border);
        
        Assert.AreEqual(board.board.Tiles[1, 0].Type, TileType.Border);
        Assert.AreEqual(board.board.Tiles[1, 1].Type, TileType.Start);
        Assert.AreEqual(board.board.Tiles[1, 2].Type, TileType.Border);

        Assert.AreEqual(board.board.Tiles[2, 0].Type, TileType.Border);
        Assert.AreEqual(board.board.Tiles[2, 1].Type, TileType.Border);
        Assert.AreEqual(board.board.Tiles[2, 2].Type, TileType.Border);

    }
}
