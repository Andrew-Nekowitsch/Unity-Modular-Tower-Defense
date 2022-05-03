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

        Assert.AreEqual(board.board.Modules[0, 0].GetModuleType(), ModuleType.Border);
        Assert.AreEqual(board.board.Modules[0, 1].GetModuleType(), ModuleType.Border);
        Assert.AreEqual(board.board.Modules[0, 2].GetModuleType(), ModuleType.Border);
        
        Assert.AreEqual(board.board.Modules[1, 0].GetModuleType(), ModuleType.Border);
        Assert.AreEqual(board.board.Modules[1, 1].GetModuleType(), ModuleType.Start);
        Assert.AreEqual(board.board.Modules[1, 2].GetModuleType(), ModuleType.Border);

        Assert.AreEqual(board.board.Modules[2, 0].GetModuleType(), ModuleType.Border);
        Assert.AreEqual(board.board.Modules[2, 1].GetModuleType(), ModuleType.Border);
        Assert.AreEqual(board.board.Modules[2, 2].GetModuleType(), ModuleType.Border);

    }
}
