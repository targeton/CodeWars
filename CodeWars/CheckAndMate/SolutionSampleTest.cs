using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWars.CheckAndMate
{

    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void Test00()
        {
            Figures.OutputBoard(TestCases.initialConfiguration);
            Assert.AreEqual(Solution.isCheck(TestCases.initialConfiguration, 0).Count, 0,
              "Initial position should not be a check");
        }

        [Test]
        public void Test01()
        {
            Figures.OutputBoard(TestCases.pawnThreatensKing);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.pawnThreatensKing, 0),
              new[] { TestCases.pawnThreatensKing[2] },
              "Pawn threatens king");
        }

        [Test]
        public void Test02()
        {
            Figures.OutputBoard(TestCases.rookThreatensKing);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.rookThreatensKing, 0),
              new[] { TestCases.rookThreatensKing[2] },
              "Rook threatens king");
        }

        [Test]
        public void Test03()
        {
            Figures.OutputBoard(TestCases.knightThreatensKing);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.knightThreatensKing, 0),
              new[] { TestCases.knightThreatensKing[2] },
              "Knight threatens king");
        }

        [Test]
        public void Test04()
        {
            Figures.OutputBoard(TestCases.bishopThreatensKing);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.bishopThreatensKing, 0),
              new[] { TestCases.bishopThreatensKing[2] },
              "Bishop threatens king");
        }

        [Test]
        public void Test05()
        {
            Figures.OutputBoard(TestCases.queenThreatensKing1);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.queenThreatensKing1, 0),
              new[] { TestCases.queenThreatensKing1[2] },
              "Queen threatens king");
        }

        [Test]
        public void Test06()
        {
            Figures.OutputBoard(TestCases.queenThreatensKing2);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.queenThreatensKing2, 0),
              new[] { TestCases.queenThreatensKing2[2] },
              "Queen threatens king");
        }

        [Test]
        public void Test07()
        {
            Figures.OutputBoard(TestCases.nonMateFool);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.nonMateFool, 0),
              new[] { TestCases.nonMateFool[9] },
              "Queen threatens king");
        }

        [Test]
        public void Test08()
        {
            Figures.OutputBoard(TestCases.nonMateFool);
            Assert.AreEqual(Solution.isMate(TestCases.nonMateFool, 0), false,
              "King can move to a safe tile");
        }

        [Test]
        public void Test09()
        {
            Figures.OutputBoard(TestCases.foolsMate);
            Assert.AreEqual(Solution.isMate(TestCases.foolsMate, 0), true,
              "Should be a mate for player 0 - Fool's mate");
        }

        [Test]
        public void Test10()
        {
            Figures.OutputBoard(TestCases.foolsMate);
            Assert.AreEqual(Solution.isMate(TestCases.foolsMate, 1), false,
              "Nothing threatens the king of player 1");
        }

        [Test]
        public void Test11()
        {
            Figures.OutputBoard(TestCases.kingsPawn);
            Assert.AreEqual(Solution.isMate(TestCases.kingsPawn, 0), true,
              "King can't move to a safe tile");
        }

        [Test]
        public void Test12()
        {
            Figures.OutputBoard(TestCases.queensPawn);
            Assert.AreEqual(Solution.isMate(TestCases.queensPawn, 0), true,
              "Pawn can't capture threatening knight because queen would have a clear line of sight");
        }

        [Test]
        public void Test13()
        {
            Figures.OutputBoard(TestCases.legalsMate);
            Assert.AreEqual(Solution.isMate(TestCases.legalsMate, 1), true,
              "King can't capture bishop because of knight 1, knight 1 and bishop block free tiles");
        }

        [Test]
        public void Test14()
        {
            Figures.OutputBoard(TestCases.interceptTest1NonMate);
            Assert.AreEqual(Solution.isMate(TestCases.interceptTest1NonMate, 0), false,
              "Should not be a mate for player 0");
        }

        [Test]
        public void Test15()
        {
            Figures.OutputBoard(TestCases.interceptTest2Mate);
            Assert.AreEqual(Solution.isMate(TestCases.interceptTest2Mate, 0), true,
              "Should be a mate for player 0");
        }

        [Test]
        public void Test16()
        {
            Figures.OutputBoard(TestCases.interceptTest3NonMate);
            Assert.AreEqual(Solution.isMate(TestCases.interceptTest3NonMate, 0), false,
              "Should not be a mate for player 0");
        }

        [Test]
        public void Test17()
        {
            Figures.OutputBoard(TestCases.interceptTest4PawnMove);
            Assert.AreEqual(Solution.isMate(TestCases.interceptTest4PawnMove, 0), false,
              "Pawn should intercept by moving");
        }

        [Test]
        public void Test18()
        {
            Figures.OutputBoard(TestCases.interceptTest4PawnDoubleMove);
            Assert.AreEqual(Solution.isMate(TestCases.interceptTest4PawnDoubleMove, 0), false,
              "Pawn should intercept by double-moving");
        }

        [Test]
        public void Test19()
        {
            Figures.OutputBoard(TestCases.interceptTest5KingCapturesPiece);
            Assert.AreEqual(Solution.isMate(TestCases.interceptTest5KingCapturesPiece, 0), false,
              "King should capture threatening piece");
        }

        [Test]
        public void Test20()
        {
            Figures.OutputBoard(TestCases.interceptTest6KingCantCapture);
            Assert.AreEqual(Solution.isMate(TestCases.interceptTest6KingCantCapture, 0), true,
              "King can't capture piece");
        }

        [Test]
        public void Test21()
        {
            Figures.OutputBoard(TestCases.doubleThreat);
            CollectionAssert.AreEquivalent(Solution.isCheck(TestCases.doubleThreat, 0),
              new[] { TestCases.doubleThreat[5], TestCases.doubleThreat[6] },
              "Double threat");
        }

        [Test]
        public void Test22()
        {
            Figures.OutputBoard(TestCases.enPassant);
            Assert.AreEqual(Solution.isMate(TestCases.enPassant, 1), false,
              "En passant");
        }

        [Test]
        public void Test23()
        {
            Figures.OutputBoard(TestCases.EnPassantFailFigures);
            Assert.AreEqual(Solution.isMate(TestCases.EnPassantFailFigures, 1), true,
              "En passant would cause check");
        }
    }

    public class TestCases
    {
        public static readonly Figure[] initialConfiguration = new[]
        {
            new Figure(FigureType.Rook, 1, new Pos(0, 0)),
            new Figure(FigureType.Knight, 1, new Pos(0, 1)),
            new Figure(FigureType.Bishop, 1, new Pos(0, 2)),
            new Figure(FigureType.Queen, 1, new Pos(0, 3)),
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(0, 5)),
            new Figure(FigureType.Knight, 1, new Pos(0, 6)),
            new Figure(FigureType.Rook, 1, new Pos(0, 7)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 0)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 1)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 2)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 3)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 4)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 5)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 6)),
            new Figure(FigureType.Pawn, 1, new Pos(1, 7)),
            new Figure(FigureType.Rook, 0, new Pos(7, 0)),
            new Figure(FigureType.Knight, 0, new Pos(7, 1)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 2)),
            new Figure(FigureType.Queen, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Knight, 0, new Pos(7, 6)),
            new Figure(FigureType.Rook, 0, new Pos(7, 7)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 0)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 1)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 2)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 3)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 6)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 7)),
        };
        public static readonly Figure[] pawnThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Pawn, 1, new Pos(6, 5), new Pos(5, 5)),
        };
        public static readonly Figure[] rookThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Rook, 1, new Pos(1, 4), new Pos(1, 5)),
        };
        public static readonly Figure[] knightThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Knight, 1, new Pos(6, 2), new Pos(4, 1)),
        };
        public static readonly Figure[] bishopThreatensKing = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(3, 0), new Pos(2, 1)),
        };
        public static readonly Figure[] queenThreatensKing1 = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Queen, 1, new Pos(1, 4), new Pos(1, 5)),
        };
        public static readonly Figure[] queenThreatensKing2 = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Queen, 1, new Pos(4, 7), new Pos(4, 6)),
        };
        public static readonly Figure[] queenThreatensKing3 = new[]
        {
            new Figure(FigureType.Pawn, 0, new Pos(4, 6)),
            new Figure(FigureType.Pawn, 0, new Pos(5, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 3)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 7)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Knight, 0, new Pos(7, 6)),
            new Figure(FigureType.Rook, 0, new Pos(7, 7)),
            new Figure(FigureType.Queen, 1, new Pos(4, 7), new Pos(0, 3)),
            new Figure(FigureType.King, 1, new Pos(0, 4)),
        };
        public static readonly Figure[] nonMateFool = new[]
        {
            new Figure(FigureType.Pawn, 0, new Pos(4, 6)),
            new Figure(FigureType.Pawn, 0, new Pos(5, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 3)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 7)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Knight, 0, new Pos(7, 6)),
            new Figure(FigureType.Rook, 0, new Pos(7, 7)),
            new Figure(FigureType.Queen, 1, new Pos(4, 7), new Pos(0, 3)),
            new Figure(FigureType.King, 1, new Pos(0, 4)),
        };
        public static readonly Figure[] foolsMate = new[]
        {
            new Figure(FigureType.Pawn, 0, new Pos(4, 6)),
            new Figure(FigureType.Pawn, 0, new Pos(5, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 3)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 7)),
            new Figure(FigureType.Queen, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Knight, 0, new Pos(7, 6)),
            new Figure(FigureType.Rook, 0, new Pos(7, 7)),
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Queen, 1, new Pos(4, 7), new Pos(0, 3)),
        };
        public static readonly Figure[] kingsPawn = new[]
        {
            new Figure(FigureType.Pawn, 0, new Pos(4, 4)),
            new Figure(FigureType.Knight, 0, new Pos(5, 2)),
            new Figure(FigureType.Pawn, 0, new Pos(5, 6)),
            new Figure(FigureType.Knight, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Queen, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Knight, 1, new Pos(5, 5), new Pos(4, 3)),
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Pawn, 1, new Pos(3, 4)),
        };
        public static readonly Figure[] queensPawn = new[]
        {
            new Figure(FigureType.Knight, 0, new Pos(6, 3)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Queen, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Queen, 1, new Pos(1, 4)),
            new Figure(FigureType.Knight, 1, new Pos(5, 3), new Pos(3, 2)),
        };
        public static readonly Figure[] legalsMate = new[]
        {
            new Figure(FigureType.Queen, 1, new Pos(0, 3)),
            new Figure(FigureType.Bishop, 1, new Pos(0, 5)),
            new Figure(FigureType.King, 1, new Pos(1, 4)),
            new Figure(FigureType.Pawn, 1, new Pos(2, 3)),
            new Figure(FigureType.Bishop, 0, new Pos(1, 5)),
            new Figure(FigureType.Knight, 0, new Pos(3, 3), new Pos(5, 2)),
            new Figure(FigureType.Knight, 0, new Pos(3, 4)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
        };
        public static readonly Figure[] interceptTest1NonMate = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(4, 1), new Pos(2, 3)),
            new Figure(FigureType.Queen, 1, new Pos(7, 0)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Rook, 0, new Pos(7, 1)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Rook, 0, new Pos(7, 5)),
        };
        public static readonly Figure[] interceptTest2Mate = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(4, 1), new Pos(2, 3)),
            new Figure(FigureType.Queen, 1, new Pos(7, 0)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Rook, 0, new Pos(7, 1)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Rook, 0, new Pos(7, 5)),
            new Figure(FigureType.Rook, 1, new Pos(4, 3)),
        };
        public static readonly Figure[] interceptTest3NonMate = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(4, 1), new Pos(2, 3)),
            new Figure(FigureType.Queen, 1, new Pos(7, 0)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Knight, 0, new Pos(7, 1)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Rook, 0, new Pos(7, 5)),
        };
        public static readonly Figure[] interceptTest4PawnMove = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(4, 1), new Pos(2, 3)),
            new Figure(FigureType.Queen, 1, new Pos(7, 0)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Queen, 0, new Pos(7, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 2)),
        };
        public static readonly Figure[] interceptTest4PawnDoubleMove = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(3, 0), new Pos(0, 3)),
            new Figure(FigureType.Queen, 1, new Pos(7, 0)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.Rook, 0, new Pos(7, 3)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 1)),
        };
        public static readonly Figure[] interceptTest5KingCapturesPiece = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Rook, 0, new Pos(7, 5)),
            new Figure(FigureType.Rook, 1, new Pos(7, 3), new Pos(0, 3)),
        };
        public static readonly Figure[] interceptTest6KingCantCapture = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Rook, 0, new Pos(7, 5)),
            new Figure(FigureType.Queen, 1, new Pos(7, 3), new Pos(6, 2)),
            new Figure(FigureType.Rook, 1, new Pos(6, 3)),
        };
        public static readonly Figure[] doubleThreat = new[]
        {
            new Figure(FigureType.King, 1, new Pos(0, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Bishop, 0, new Pos(7, 5)),
            new Figure(FigureType.Bishop, 1, new Pos(4, 1)),
            new Figure(FigureType.Rook, 1, new Pos(7, 2), new Pos(5, 2)),
        };
        public static readonly Figure[] enPassant = new[]
        {
            new Figure(FigureType.King, 1, new Pos(3, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(4, 4), new Pos(6, 4)),
            new Figure(FigureType.Pawn, 0, new Pos(6, 5)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Knight, 0, new Pos(5, 2)),
            new Figure(FigureType.Pawn, 1, new Pos(4, 3)),
            new Figure(FigureType.Knight, 1, new Pos(3, 3)),
            new Figure(FigureType.Pawn, 1, new Pos(3, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(2, 4)),
            new Figure(FigureType.Rook, 1, new Pos(2, 5)),
            new Figure(FigureType.Queen, 0, new Pos(5, 6)),
        };
        public static readonly Figure[] EnPassantFailFigures = new[]
        {
            new Figure(FigureType.King, 1, new Pos(3, 5)),
            new Figure(FigureType.Pawn, 0, new Pos(4, 4), new Pos(6, 4)),
            new Figure(FigureType.Rook, 0, new Pos(6, 5)),
            new Figure(FigureType.King, 0, new Pos(7, 4)),
            new Figure(FigureType.Knight, 0, new Pos(5, 2)),
            new Figure(FigureType.Pawn, 1, new Pos(4, 5)),
            new Figure(FigureType.Knight, 1, new Pos(3, 3)),
            new Figure(FigureType.Pawn, 1, new Pos(3, 4)),
            new Figure(FigureType.Bishop, 1, new Pos(2, 4)),
            new Figure(FigureType.Rook, 1, new Pos(2, 5)),
            new Figure(FigureType.Queen, 0, new Pos(5, 6)),
        };
    }
}
