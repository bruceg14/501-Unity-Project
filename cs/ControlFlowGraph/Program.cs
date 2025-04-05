using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;

class CFG_Node {
    public int id;
    public string content;
}

class CFG_Edge {
    public CFG_Node source;
    public CFG_Node target;
}

class CFG_Graph {
    public List<CFG_Node> nodes;
    public List<CFG_Edge> edges;
    
    public CFG_Graph() {
        nodes = new List<CFG_Node>();
        edges = new List<CFG_Edge>();
    }

    public CFG_Graph(List<CFG_Node> nodes, List<CFG_Edge> edges) {
        this.nodes = nodes;
        this.edges = edges;
    }

    public void AddNode(CFG_Node node) {
        // Console.WriteLine($"Adding node, Node {node.id}: {node.content}");
        nodes.Add(node);
    }

    public void AddEdge(CFG_Edge edge) {
        edges.Add(edge);
    }

    public void PrintGraph() {
        // foreach (var node in nodes) {
        //     Console.WriteLine($"Node {node.id}: {node.content}");
        // }
        // foreach (var edge in edges) {
        //     Console.WriteLine($"Edge from {edge.source.id} to {edge.target.id}");
        // }
        foreach (var edge in edges) {
            Console.WriteLine($"{edge.source.id} -> {edge.target.id};");
        }
    }
}


class CFG{
    public CFG_Graph graph = new CFG_Graph();

    public CFG_Node RecursiveCFGCreation(SyntaxList<StatementSyntax> statements, CFG_Node node) {
        if(statements.Count == 0) {
            return null;
        }
        foreach (var statement in statements) {
            if (statement is IfStatementSyntax) {
                var ifNode = new CFG_Node { id = graph.nodes.Count, content = "If statement" };
                graph.AddNode(ifNode);
                graph.AddEdge(new CFG_Edge { source = node, target = ifNode });
                
                var ifStatement = (IfStatementSyntax)statement;

                // Check the then part of the if statement
                var thenBlock = ifStatement.Statement as BlockSyntax;
                var thenNode = new CFG_Node { id = graph.nodes.Count, content = "Then block" };
                graph.AddNode(thenNode);
                graph.AddEdge(new CFG_Edge { source = ifNode, target = thenNode });

                var afterIfNode = new CFG_Node { id = graph.nodes.Count, content = "After if" };
                graph.AddNode(afterIfNode);

                Console.WriteLine($"Then block: {thenBlock}");
                // Create nodes for the then and else blocks
                if(thenBlock.Statements.Count > 0) {
                    var exitThenNode = RecursiveCFGCreation(thenBlock.Statements, thenNode); 
                    if (exitThenNode != null) {
                        graph.AddEdge(new CFG_Edge { source = exitThenNode, target = afterIfNode });
                    }
                }

                // Check the else part of the if statement
                var elseBlock = ifStatement.Else?.Statement as BlockSyntax;
                if(elseBlock!= null && elseBlock.Statements.Count > 0) {
                    var elseNode = new CFG_Node { id = graph.nodes.Count, content = "Else block" };
                    graph.AddNode(elseNode);
                    graph.AddEdge(new CFG_Edge { source = ifNode, target = elseNode });

                    var exitElseNode = RecursiveCFGCreation(elseBlock.Statements, elseNode);
                    graph.AddEdge(new CFG_Edge { source = exitElseNode, target = afterIfNode });
                    
                } else {
                    graph.AddEdge(new CFG_Edge { source = ifNode, target = afterIfNode });
                }

                node = afterIfNode;
            } else if (statement is WhileStatementSyntax) {
                var whileNode = new CFG_Node { id = graph.nodes.Count, content = "While statement" };
                graph.AddNode(whileNode);
                graph.AddEdge(new CFG_Edge { source = node, target = whileNode });

                var whileStatement = (WhileStatementSyntax)statement;
                var whileBlock = whileStatement.Statement as BlockSyntax;
                var whileBodyNode = new CFG_Node { id = graph.nodes.Count, content = "While body" };
                graph.AddNode(whileBodyNode);
                graph.AddEdge(new CFG_Edge { source = whileNode, target = whileBodyNode });


                var afterWhileNode = new CFG_Node { id = graph.nodes.Count, content = "After while" };
                graph.AddNode(afterWhileNode);
                graph.AddEdge(new CFG_Edge { source = whileNode, target = afterWhileNode });

                if (whileBlock!= null && whileBlock.Statements.Count > 0) {
                    var exitWhileNode = RecursiveCFGCreation(whileBlock.Statements, whileBodyNode);
                    graph.AddNode(exitWhileNode);
                    graph.AddEdge(new CFG_Edge { source = exitWhileNode, target = whileNode });
                }
                
                node = afterWhileNode;
            } else if (statement is ForStatementSyntax){ // Same logic as while
                var forNode = new CFG_Node { id = graph.nodes.Count, content = "For statement"};
                graph.AddNode(forNode);
                graph.AddEdge(new CFG_Edge { source = node, target = forNode});

                var forStatement = (ForStatementSyntax) statement;
                var forBlock = forStatement.Statement as BlockSyntax;

                var forBodyNode = new CFG_Node { id = graph.nodes.Count, content = "For body" };
                graph.AddNode(forBodyNode);
                graph.AddEdge(new CFG_Edge { source = forNode, target = forBodyNode });
                var afterForNode = new CFG_Node { id = graph.nodes.Count, content = "After for" };
                graph.AddNode(afterForNode);
                graph.AddEdge(new CFG_Edge { source = forNode, target = afterForNode });
                if (forBlock!= null && forBlock.Statements.Count > 0) {
                    var exitForNode = RecursiveCFGCreation(forBlock.Statements, forBodyNode);
                    graph.AddNode(exitForNode);
                    graph.AddEdge(new CFG_Edge { source = exitForNode, target = forNode });
                }
                
                node = afterForNode;
            } else if (statement is SwitchStatementSyntax) {
                var switchStatement = (SwitchStatementSyntax) statement;

                var switchNode = new CFG_Node {id = graph.nodes.Count, content = "Switch"};
                graph.AddNode(switchNode);
                graph.AddEdge(new CFG_Edge {source = node, target = switchNode});

                var afterSwitchNode = new CFG_Node { id = graph.nodes.Count, content = "After Switch" };
                graph.AddNode(afterSwitchNode);

                var switchBlock = switchStatement.Sections; // SyntaxList<SwitchSectionSyntax>
                foreach (var switchSec in switchBlock) {
                    var switchSectionNode = new CFG_Node { id = graph.nodes.Count, content = "One switch branch" };
                    graph.AddNode(switchSectionNode);
                    graph.AddEdge(new CFG_Edge { source = switchNode, target = switchSectionNode });
                    
                    var switchSectionBody = switchSec.Statements ;
                    Console.WriteLine($"switch section body: {switchSectionBody}");
                    Console.WriteLine($"switch section body: {switchSectionBody.GetType()}");
                    var exitSwitchSectionNode = RecursiveCFGCreation(switchSectionBody, switchSectionNode);
                    graph.AddEdge(new CFG_Edge { source = exitSwitchSectionNode, target = afterSwitchNode });
                }

                node = afterSwitchNode;

            } else {     
                var statementNode = new CFG_Node { id = graph.nodes.Count, content = statement.ToString() };
                graph.AddNode(statementNode);
                graph.AddEdge(new CFG_Edge { source = node, target = statementNode });
                node = statementNode;
                
            }
            
        }

        return node;
    }

    public static void Main(string[] args)
    {
        var sourceCode = File.ReadAllText("Test.cs");
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var root = syntaxTree.GetRoot();
        
        var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
        foreach (var method in methods)
        {   
            var controlFlowGraph = new CFG();
            var entryNode = new CFG_Node { id = 0, content = "Entry" };
            controlFlowGraph.graph.AddNode(entryNode);
            controlFlowGraph.RecursiveCFGCreation(method.Body.Statements, entryNode);
            controlFlowGraph.graph.PrintGraph();
        }
    }
}