//Participantes: Davi Farias, Eduardo Cardoso, Vinicius, Pedro Vilela e Rafael

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static List<Produto> list = new List<Produto>();
        static void Main(string[] args)
        {
            bool exit = false;

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            setProducts();

            while (!exit)
            {
                Globals.H = 0;
                Globals.V = 0;
                Console.Clear();
                draw();

                Console.SetCursorPosition(42, 2);
                Console.WriteLine("======  INFO STORE MANAGER  ======");
                Console.SetCursorPosition(31 - Globals.H, 4);
                Console.WriteLine("1.Cadastrar produto");
                Console.SetCursorPosition(31 - Globals.H, 5);
                Console.WriteLine("2.Consultar produto");
                Console.SetCursorPosition(31 - Globals.H, 6);
                Console.WriteLine("3.Listar todos os produtos");
                Console.SetCursorPosition(31 - Globals.H, 7);
                Console.WriteLine("4.Realizar Venda");
                Console.SetCursorPosition(31 - Globals.H, 8);
                Console.WriteLine("5.Controle de Estoque");
                Console.SetCursorPosition(31 - Globals.H, 9);
                Console.WriteLine("6.Gerenciar Promoção");
                Console.SetCursorPosition(31 - Globals.H, 10);
                Console.WriteLine("7.Relatórios do Dia");
                Console.SetCursorPosition(31 - Globals.H, 11);
                Console.WriteLine("8.Sair");
                Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(ReadLineLimited(1), out int op))
                {
                    switch (op)
                    {
                        case 1:
                            one();
                            break;
                        case 2:
                            two();
                            break;
                        case 3:
                            three();
                            break;
                        case 4:
                            four();
                            break;
                        case 5:
                            five();
                            break;
                        case 6:
                            six();
                            break;
                        case 7:
                            seven();
                            break;
                        case 8:
                            exit = true;
                            Console.Clear();
                            break;
                        default:
                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                            Console.WriteLine("Opção inválida.");
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                    Console.WriteLine("Valor inválido.");
                    Globals.En = true;
                    ReadLineLimited(1);
                }
            }
        }

        static string ReadLineLimited(int maxLength)
        {
            var input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    Globals.Up = false;
                    Globals.En = false;
                    break;
                }

                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input.Length--;
                        Console.Write("\b \b");
                    }
                }
                else if (input.Length < maxLength)
                {
                    if (Globals.En != true)
                    {
                        if (Globals.Up == true)
                        {
                            char upperChar = char.ToUpperInvariant(key.KeyChar);
                            input.Append(upperChar);
                            Console.Write(upperChar);
                        }
                        else
                        {
                            char upperChar = key.KeyChar;
                            input.Append(upperChar);
                            Console.Write(upperChar);
                        }
                    }
                }
            }
            return input.ToString();
        }

        static void draw()
        {
            char cSED = '\u2554'; // ╔
            char cSDD = '\u2557'; // ╗
            char cIED = '\u255A'; // ╚
            char cIDD = '\u255D'; // ╝
            char bHD = '\u2550';  // ═
            char bVD = '\u2551';  // ║
            char cVED = '\u2560'; // ╠
            char cVDD = '\u2563'; // ╣

            Console.SetCursorPosition(30 - Globals.H, 1);
            Console.Write(cSED);
            Console.SetCursorPosition(30 - Globals.H, 15 + Globals.V);
            Console.Write(cIED);
            Console.SetCursorPosition(87 + Globals.H, 1);
            Console.Write(cSDD);
            Console.SetCursorPosition(87 + Globals.H, 15 + Globals.V);
            Console.Write(cIDD);

            for (int i = 0; i <= 55 + Globals.H * 2; i++)
            {
                Console.SetCursorPosition(31 + i - Globals.H, 1);
                Console.Write(bHD);
                Console.SetCursorPosition(31 + i - Globals.H, 3);
                Console.Write(bHD);
                Console.SetCursorPosition(31 + i - Globals.H, 13 + Globals.V);
                Console.Write(bHD);
                Console.SetCursorPosition(31 + i - Globals.H, 15 + Globals.V);
                Console.Write(bHD);
            }

            for (int i = 0 - Globals.V; i <= 12; i++)
            {
                if (i == 1 - Globals.V ^ i == 11)
                {
                    Console.SetCursorPosition(30 - Globals.H, 2 + i + Globals.V);
                    Console.Write(cVED);
                    Console.SetCursorPosition(87 + Globals.H, 2 + i + Globals.V);
                    Console.Write(cVDD);
                    i++;
                }
                Console.SetCursorPosition(30 - Globals.H, 2 + i + Globals.V);
                Console.Write(bVD);
                Console.SetCursorPosition(87 + Globals.H, 2 + i + Globals.V);
                Console.Write(bVD);
            }
        }

        static void one()
        {
            Globals.H = 7;
            Globals.V = -2;
            Console.Clear();
            draw();
            Console.SetCursorPosition(41, 2);
            Console.WriteLine("======  CADASTRO DE PRODUTOS  ======");
            Console.SetCursorPosition(31 - Globals.H, 4);
            Console.Write("Digite o código do produto: ");

            if (int.TryParse(ReadLineLimited(6), out int vrid))
            {
                if (!list.Exists(p => p.Id == vrid))
                {
                    Produto newProd = new Produto();
                    newProd.Id = vrid;
                    Console.SetCursorPosition(31 - Globals.H, 5);
                    Console.Write("Nome do produto: ");
                    String N = ReadLineLimited(20);
                    if (N != "")
                    {
                        newProd.Name = N;
                        Console.SetCursorPosition(31 - Globals.H, 6);
                        Console.Write("Categoria (1.Informática, 2.Acessórios, 3.Papelaria, 4.Telefonia): ");
                        if (int.TryParse(ReadLineLimited(1), out int Ctl))
                        {
                            switch (Ctl)
                            {
                                case 1:
                                    newProd.Ctl = "Informática";
                                    break;
                                case 2:
                                    newProd.Ctl = "Acessórios";
                                    break;
                                case 3:
                                    newProd.Ctl = "Papelaria";
                                    break;
                                case 4:
                                    newProd.Ctl = "Telefonia";
                                    break;
                                default:
                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                    Console.WriteLine("Categoria inexistente.");
                                    Globals.En = true;
                                    ReadLineLimited(1);
                                    return;
                            }
                            Console.SetCursorPosition(31 - Globals.H, 7);
                            Console.Write("Preço de custo: ");
                            if (int.TryParse(ReadLineLimited(7), out int Pc) && Pc > 0)
                            {
                                newProd.Pc = Pc;
                                Console.SetCursorPosition(31 - Globals.H, 8);
                                Console.Write("Margem de lucro (%): ");
                                if (int.TryParse(ReadLineLimited(3), out int Ml) && Ml >= 0)
                                {
                                    newProd.Ml = Ml;
                                    Console.SetCursorPosition(31 - Globals.H, 9);
                                    Console.Write("Quantidade em estoque: ");
                                    if (int.TryParse(ReadLineLimited(7), out int Qe) && Qe >= 0)
                                    {
                                        newProd.Qe = Qe;
                                        newProd.Vn = 0;
                                        list.Add(newProd);
                                        Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                                        Console.WriteLine($"Produto cadastrado com sucesso! Preço final de venda: R${newProd.Pc + (newProd.Pc * newProd.Ml / 100.0):F2}");
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.Write("Quantidade inaceitável.");
                                    }
                                }
                                else
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                    Console.Write("Margem inaceitável.");
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                Console.Write("Preço inaceitável.");
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                            Console.WriteLine("Categoria inexistente.");
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                        Console.WriteLine("Nome inaceitável.");
                    }
                }
                else
                {
                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                    Console.WriteLine("Produto já cadastrado");
                }
            }
            else
            {
                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                Console.WriteLine("Código inválido.");
            }
            Globals.En = true;
            ReadLineLimited(1);
        }

        static void two()
        {
            Globals.H = 0;
            Globals.V = 0;
            Console.Clear();
            draw();
            Console.SetCursorPosition(41, 2);
            Console.WriteLine("======  CONSULTA DE PRODUTOS  ======");
            Console.SetCursorPosition(31 - Globals.H, 4);
            Console.Write("Informe o código do produto: ");

            if (int.TryParse(ReadLineLimited(6), out int searchId))
            {
                Produto found = list.FirstOrDefault(p => p.Id == searchId);
                if (found != null)
                {
                    Console.SetCursorPosition(31 - Globals.H, 5);
                    Console.WriteLine("Produto encontrado:");
                    Console.SetCursorPosition(31 - Globals.H, 7); Console.WriteLine($"Nome: {found.Name}");
                    Console.SetCursorPosition(31 - Globals.H, 8); Console.WriteLine($"Categoria: {found.Ctl}");
                    Console.SetCursorPosition(31 - Globals.H, 9); Console.WriteLine($"Preço: R${found.Pc}");
                    Console.SetCursorPosition(31 - Globals.H, 10); Console.WriteLine($"% de Lucro: {found.Ml}");
                    Console.SetCursorPosition(31 - Globals.H, 11);
                    Console.WriteLine($"Estoque: {found.Qe} {(found.Qe == 1 ? "unidade" : "unidades")}");
                    Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V); Console.WriteLine("Pressione ENTER para continuar...");
                }
                else
                {
                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                    Console.WriteLine("Produto não encontrado.");
                }
            }
            else
            {
                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                Console.WriteLine("Código inválido.");
            }
            Globals.En = true;
            ReadLineLimited(1);
        }

        static void three()
        {
            Globals.H = 22;
            Globals.V = 0;
            Console.Clear();
            Console.SetCursorPosition(36, 2);
            Console.WriteLine("======  LISTA DE PRODUTOS CADASTRADOS  ======");
            Console.SetCursorPosition(31 - Globals.H, 4);
            Console.WriteLine("Código:    ||    Nome:    ||    Categoria:    ||    Preço:    ||    % de Lucro:    ||    Estoque: ");
            if (list.Count != 0)
            {
                foreach (var p in list)
                {
                    Console.SetCursorPosition(31 - Globals.H, 5 + Globals.V);
                    Console.WriteLine(p.Id);

                    Console.SetCursorPosition(50 - Globals.H - (p.Name.Length / 2), 5 + Globals.V);
                    Console.WriteLine(p.Name);

                    Console.SetCursorPosition(63 - Globals.H, 5 + Globals.V);
                    Console.WriteLine(p.Ctl);

                    Console.SetCursorPosition(83 - Globals.H, 5 + Globals.V);
                    Console.WriteLine(p.Pc);

                    Console.SetCursorPosition(99 - Globals.H, 5 + Globals.V);
                    Console.WriteLine(p.Ml);

                    Console.SetCursorPosition(120 - Globals.H, 5 + Globals.V);
                    Console.WriteLine(p.Qe);
                    Globals.V++;
                }
                Console.SetCursorPosition(31 - Globals.H, 7 + Globals.V);
                Console.WriteLine($"Total de produtos cadastrados: {list.Count}");
            }
            else
            {
                Console.SetCursorPosition(31 - Globals.H, 7);
                Console.WriteLine("Nenhum produto cadastrado.");
            }
            Console.SetCursorPosition(31 - Globals.H, 9 + Globals.V);
            Console.WriteLine("Pressione ENTER para continuar...");
            Globals.V -= 5;
            draw();
            Globals.En = true;
            ReadLineLimited(1);
        }
        static void four()
        {
            Globals.H = 1;
            Globals.V = 2;
            Console.Clear();
            draw();
            Console.SetCursorPosition(44, 2);
            Console.WriteLine("======  REALIZAR VENDA  ======");
            Console.SetCursorPosition(31 - Globals.H, 4);
            Console.Write("Código do produto: ");

            if (int.TryParse(ReadLineLimited(6), out int searchId))
            {
                Produto found = list.FirstOrDefault(p => p.Id == searchId);
                if (found != null)
                {
                    double Valor = found.Pc + (found.Pc * found.Ml / 100.0);
                    double Pc;
                    Console.SetCursorPosition(31 - Globals.H, 5); Console.WriteLine($"Produto encontrado: {found.Name}");
                    Console.SetCursorPosition(31 - Globals.H, 6); Console.WriteLine($"Categoria: {found.Ctl}");
                    Console.SetCursorPosition(31 - Globals.H, 7); Console.WriteLine($"Preço original: R${Valor}");
                    Console.SetCursorPosition(31 - Globals.H, 8);
                    Console.WriteLine($"Estoque disponível: {found.Qe} {(found.Qe == 1 ? "unidade" : "unidades")}");
                    Console.SetCursorPosition(31 - Globals.H, 9); Console.Write("Quantidade desejada: ");

                    if (int.TryParse(ReadLineLimited(7), out int Qd) && Qd <= found.Qe)
                    {
                        if (Qd > 0)
                        {
                            if (Globals.Pc == found.Ctl)
                            {
                                Console.SetCursorPosition(31 - Globals.H, 11);
                                Console.WriteLine($"PROMOÇÃO ATIVA: {Globals.Pp}% de desconto na categoria {Globals.Pc}!");
                                Console.SetCursorPosition(31 - Globals.H, 12);
                                Console.WriteLine($"Preço com desconto: R${Valor - (Valor * Globals.Pp / 100.0)} por unidade");
                                Console.SetCursorPosition(31 - Globals.H, 13); Console.WriteLine($"Valor total: R${Pc = (Valor - (Valor * Globals.Pp / 100.0)) * Qd}");
                            }
                            else
                            {
                                Console.SetCursorPosition(31 - Globals.H, 10); Console.WriteLine($"Valor total: R${Pc = Valor * Qd}");
                            }
                            Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V); Console.Write("Confirmar venda? (S/N): ");
                            Globals.Up = true;
                            string V = ReadLineLimited(1);
                            if (V == "S")
                            {
                                if (Globals.Pc == found.Ctl)
                                {
                                    Globals.Vd += 1;
                                    Globals.Dt += (Valor * Globals.Pp / 100.0) * Qd;
                                }
                                found.Qe = found.Qe - Qd;
                                found.Vn += Qd;
                                Globals.Fb += Pc;
                                Globals.Vv = Pc;
                                Globals.Vi = found.Id;
                                Globals.Vn += 1;
                                if (Globals.Ma < Globals.Vv)
                                {
                                    Globals.Ma = Globals.Vv;
                                }
                                if (Globals.Me > Globals.Vv ^ Globals.Me == 0)
                                {
                                    Globals.Me = Globals.Vv;
                                }

                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                Console.WriteLine($"Venda realizada com sucesso! Novo estoque: {found.Qe} {(found.Qe == 1 ? "unidade" : "unidades")}");
                            }
                            else
                            {
                                if (V != "N")
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                    Console.WriteLine("Código inválido.");
                                }
                                else
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                    Console.WriteLine("Venda cancelada.");
                                }
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                            Console.WriteLine("Quantidade inválida.");
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                        Console.WriteLine("Estoque insuficiente.");
                    }
                }
                else
                {
                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                    Console.WriteLine("Produto não encontrado.");
                }
            }
            else
            {
                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                Console.WriteLine("Código inválido.");
            }
            Globals.En = true;
            ReadLineLimited(1);
        }
        static void five()
        {
            bool exit = false;
            while (!exit)
            {
                Globals.H = 0;
                Globals.V = 0;
                Console.Clear();
                draw();
                Console.SetCursorPosition(41, 2);
                Console.WriteLine("======  CONTROLE DE ESTOQUES  ======");
                Console.SetCursorPosition(31 - Globals.H, 4);
                Console.WriteLine("1.Adicionar ao Estoque");
                Console.SetCursorPosition(31 - Globals.H, 6);
                Console.WriteLine("2.Remover do Estoque");
                Console.SetCursorPosition(31 - Globals.H, 8);
                Console.WriteLine("3.Verificar Estoque Baixo");
                Console.SetCursorPosition(31 - Globals.H, 10);
                Console.WriteLine("4.Voltar ao Menu Principal");
                Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(ReadLineLimited(1), out int op))
                {
                    switch (op)
                    {
                        case 1:
                            Globals.H = 0;
                            Globals.V = 1;
                            Console.Clear();
                            draw();
                            Console.SetCursorPosition(41, 2);
                            Console.WriteLine("======  ADICIONAR AO ESTOQUE  ======");
                            Console.SetCursorPosition(31 - Globals.H, 4);
                            Console.Write("Código do produto: ");
                            if (int.TryParse(ReadLineLimited(6), out int searchId))
                            {
                                Produto found = list.FirstOrDefault(p => p.Id == searchId);
                                if (found != null)
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 5); Console.WriteLine($"Produto: {found.Name}");
                                    Console.SetCursorPosition(31 - Globals.H, 6);
                                    Console.WriteLine($"Estoque atual: {found.Qe} {(found.Qe == 1 ? "unidade" : "unidades")}");
                                    Console.SetCursorPosition(31 - Globals.H, 7); Console.Write("Quantidade a adicionar: ");
                                    if (int.TryParse(ReadLineLimited(7), out int Qa) && Qa > 0)
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 8); Console.WriteLine("Motivo da entrada: ");
                                        Console.SetCursorPosition(31 - Globals.H, 9); Console.WriteLine("1.Compra de fornecedor");
                                        Console.SetCursorPosition(31 - Globals.H, 10); Console.WriteLine("2.Devolução de cliente");
                                        Console.SetCursorPosition(31 - Globals.H, 11); Console.WriteLine("3.Ajuste de inventário");
                                        Console.SetCursorPosition(31 - Globals.H, 12); Console.Write("Escolha: ");
                                        if (int.TryParse(ReadLineLimited(1), out int op2) && op2 >= 1 && op2 <= 3)
                                        {
                                            Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V); Console.Write($"Confirmar adição de {Qa} {(Qa == 1 ? "unidade?" : "unidades?")} (S/N): ");
                                            Globals.Up = true;
                                            string V = ReadLineLimited(1);
                                            if (V == "S")
                                            {
                                                found.Qe += Qa;
                                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                Console.WriteLine($"Estoque atualizado! Novo saldo: {found.Qe} {(found.Qe == 1 ? "unidade" : "unidades")}");
                                            }
                                            else
                                            {
                                                if (V != "N")
                                                {
                                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                    Console.WriteLine("Código inválido.");
                                                }
                                                else
                                                {
                                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                    Console.WriteLine("Adição cancelada.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                            Console.WriteLine("Código inválido.");
                                        }
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.WriteLine("Quantidade inválida.");
                                    }
                                }
                                else
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                    Console.WriteLine("Produto não encontrado.");
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                Console.WriteLine("Código inválido.");
                            }

                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 2:
                            Globals.H = 0;
                            Globals.V = 2;
                            Console.Clear();
                            draw();
                            Console.SetCursorPosition(42, 2);
                            Console.WriteLine("======  REMOVER DO ESTOQUE  ======");
                            Console.SetCursorPosition(31 - Globals.H, 4);
                            Console.Write("Código do produto: ");

                            if (int.TryParse(ReadLineLimited(6), out int searchId2))
                            {
                                Produto found = list.FirstOrDefault(p => p.Id == searchId2);
                                if (found != null)
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 5); Console.WriteLine($"Produto: {found.Name}");
                                    Console.SetCursorPosition(31 - Globals.H, 6);
                                    Console.WriteLine($"Estoque atual: {found.Qe} {(found.Qe == 1 ? "unidade" : "unidades")}");
                                    Console.SetCursorPosition(31 - Globals.H, 7); Console.Write("Quantidade a remover: ");
                                    int.TryParse(ReadLineLimited(7), out int Qr);
                                    if (Qr <= found.Qe && Qr > 0)
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 8); Console.WriteLine("Motivo da entrada: ");
                                        Console.SetCursorPosition(31 - Globals.H, 9); Console.WriteLine("1.Venda manual");
                                        Console.SetCursorPosition(31 - Globals.H, 10); Console.WriteLine("2.Produto danificado");
                                        Console.SetCursorPosition(31 - Globals.H, 11); Console.WriteLine("3.Transferência");
                                        Console.SetCursorPosition(31 - Globals.H, 12); Console.WriteLine("4.Ajuste de inventário");
                                        Console.SetCursorPosition(31 - Globals.H, 13); Console.Write("Escolha: ");
                                        if (int.TryParse(ReadLineLimited(1), out int op2) && op2 >= 1 && op2 <= 4)
                                        {
                                            Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V); Console.Write($"Confirmar adição de {Qr} {(Qr == 1 ? "unidade?" : "unidades?")} (S/N): ");
                                            Globals.Up = true;
                                            string V = ReadLineLimited(1);
                                            if (V == "S")
                                            {
                                                found.Qe -= Qr;
                                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                Console.WriteLine($"Estoque atualizado! Novo saldo: {found.Qe} {(found.Qe == 1 ? "unidade" : "unidades")}");
                                            }
                                            else
                                            {
                                                if (V != "N")
                                                {
                                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                    Console.WriteLine("Código inválido.");
                                                }
                                                else
                                                {
                                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                    Console.WriteLine("Remoção cancelada.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                            Console.WriteLine("Código inválido.");
                                        }
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.WriteLine("Quantidade inválida.");
                                    }
                                }
                                else
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                    Console.WriteLine("Produto não encontrado.");
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                Console.WriteLine("Código inválido.");
                            }
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 3:
                            Globals.H = 0;
                            Globals.V = 0;
                            Console.Clear();
                            Console.SetCursorPosition(37, 2);
                            Console.WriteLine("======  VERIFICAÇÃO DE ESTOQUE BAIXO  ======");

                            Console.SetCursorPosition(31 - Globals.H, 4);
                            Console.WriteLine("Código:        ||        Nome:        ||    Estoque: ");
                            if (list.Count != 0)
                            {
                                foreach (var p in list)
                                {
                                    if (p.Qe < 5)
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 5 + Globals.V);
                                        Console.WriteLine(p.Id);

                                        Console.SetCursorPosition(58 - Globals.H - (p.Name.Length / 2), 5 + Globals.V);
                                        Console.WriteLine(p.Name);

                                        Console.SetCursorPosition(75 - Globals.H, 5 + Globals.V);
                                        Console.WriteLine($"{p.Qe} {(p.Qe == 1 ? "unidade" : "unidades")}");

                                        Globals.V++;
                                    }
                                }
                                Console.SetCursorPosition(31 - Globals.H, 7 + Globals.V);
                                Console.WriteLine($"Total de produtos em alerta: {Globals.V}");
                            }
                            else
                            {
                                Console.SetCursorPosition(31 - Globals.H, 7);
                                Console.WriteLine("Nenhum produto cadastrado.");
                            }
                            Console.SetCursorPosition(31 - Globals.H, 9 + Globals.V);
                            Console.WriteLine("Pressione ENTER para continuar...");
                            Globals.V -= 5;
                            draw();
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                            Console.WriteLine("Opção inválida.");
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                    Console.WriteLine("Valor inválido.");
                    Globals.En = true;
                    ReadLineLimited(1);
                }
            }
        }

        static void six()
        {
            Stopwatch stop = new Stopwatch();
            bool exit = false;

            while (!exit)
            {
                Globals.H = 0;
                Globals.V = 0;
                Console.Clear();
                draw();
                Console.SetCursorPosition(42, 2);
                Console.WriteLine("====== GERENCIAR PROMOÇÃO ======");

                Console.SetCursorPosition(31 - Globals.H, 4); Console.WriteLine("1.Ativar Promoção");
                Console.SetCursorPosition(31 - Globals.H, 6); Console.WriteLine("2.Consultar Promoção Ativa");
                Console.SetCursorPosition(31 - Globals.H, 8); Console.WriteLine("3.Desativar Promoção");
                Console.SetCursorPosition(31 - Globals.H, 10); Console.WriteLine("4.Voltar ao Menu Principal");
                Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V); Console.Write("Escolha uma opção: ");

                if (stop.ElapsedMilliseconds > Globals.Tm && Globals.Pc != null)
                {
                    Globals.Pc = null;
                    Globals.Pp = 0;
                    stop.Stop();
                    stop.Reset();
                }

                if (int.TryParse(ReadLineLimited(1), out int op))
                {
                    switch (op)
                    {
                        case 1:
                            Globals.H = 9;
                            Globals.V = 2;
                            Console.Clear();
                            draw();
                            Console.SetCursorPosition(43, 2);
                            Console.WriteLine("====== ATIVAR PROMOÇÕES ======");

                            if (Globals.Pc != null)
                            {
                                Globals.H = 0;
                                Globals.V = -2;
                                Console.Clear();
                                draw();
                                Console.SetCursorPosition(43, 2);
                                Console.WriteLine("====== ATIVAR PROMOÇÕES ======");
                                Console.SetCursorPosition(31 - Globals.H, 4); Console.WriteLine("Status atual: Ativa");
                                Console.SetCursorPosition(31 - Globals.H, 5); Console.WriteLine($"{Globals.Pc} ({Globals.Pp}%)");
                                Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                                Console.WriteLine("Pressione ENTER para continuar...");
                                Globals.En = true;
                                ReadLineLimited(1);
                                break;
                            }
                            else
                            {
                                Console.SetCursorPosition(31 - Globals.H, 4); Console.WriteLine("Status atual: Nenhuma promoção ativa");
                                Console.SetCursorPosition(31 - Globals.H, 5); Console.WriteLine("Categoria para promoção:");
                                Console.SetCursorPosition(31 - Globals.H, 6); Console.WriteLine("1.Informática");
                                Console.SetCursorPosition(31 - Globals.H, 7); Console.WriteLine("2.Acessórios");
                                Console.SetCursorPosition(31 - Globals.H, 8); Console.WriteLine("3.Papelaria");
                                Console.SetCursorPosition(31 - Globals.H, 9); Console.WriteLine("4.Telefonia");
                                Console.SetCursorPosition(31 - Globals.H, 10); Console.Write("Escolha: ");

                                string ct = null;
                                int.TryParse(ReadLineLimited(1), out int op1);
                                switch (op1)
                                {
                                    case 1:
                                        ct = "Informática";
                                        break;
                                    case 2:
                                        ct = "Acessórios";
                                        break;
                                    case 3:
                                        ct = "Papelaria";
                                        break;
                                    case 4:
                                        ct = "Telefonia";
                                        break;
                                    default:
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.WriteLine("Código inválido.");
                                        Globals.En = true;
                                        ReadLineLimited(1);
                                        six();
                                        return;
                                }
                                Console.SetCursorPosition(31 - Globals.H, 12); Console.Write("Desconto (%): ");
                                if (int.TryParse(ReadLineLimited(2), out int ds) && ds > 0)
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 13); Console.Write("Duração (horas): ");
                                    if (int.TryParse(ReadLineLimited(2), out int hr) && hr > 0)
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                                        Console.Write($"Confirmar promoção de {ds}% na categoria {ct} por {hr} {(hr == 1 ? "hora" : "horas")}? (S/N): ");
                                        Globals.Up = true;
                                        string V = ReadLineLimited(1);
                                        if (V == "S")
                                        {
                                            Globals.Pc = ct;
                                            Globals.Pp = ds;
                                            Globals.Tm = hr * 3600000;
                                            stop.Reset();
                                            stop.Start();
                                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                            Console.WriteLine("Promoção ativada com sucesso!");
                                        }
                                        else
                                        {
                                            if (V != "N")
                                            {
                                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                Console.WriteLine("Código inválido.");
                                            }
                                            else
                                            {
                                                Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                                Console.WriteLine("Ativação cancelada.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.WriteLine("Valor inválido.");
                                    }
                                }
                                else
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                    Console.WriteLine("Valor inválido.");
                                }
                            }
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 2:
                            Globals.H = 0;
                            Globals.V = 0;
                            Console.Clear();
                            draw();
                            Console.SetCursorPosition(38, 2);
                            Console.WriteLine("====== CONSULTA DE PROMOÇÃO ATIVA ======");

                            if (Globals.Pc != null)
                            {
                                double restante = (Globals.Tm - stop.ElapsedMilliseconds) / 3600000.0;
                                int h = (int)restante;
                                int m = (int)((restante - h) * 60);

                                Console.SetCursorPosition(31 - Globals.H, 4); Console.WriteLine("Status: Ativa");
                                Console.SetCursorPosition(31 - Globals.H, 5); Console.WriteLine($"Categoria: {Globals.Pc}");
                                Console.SetCursorPosition(31 - Globals.H, 6); Console.WriteLine($"Desconto: {Globals.Pp}%");
                                Console.SetCursorPosition(31 - Globals.H, 7); Console.WriteLine($"Tempo restante: {h} {(h == 1 ? "hora" : "horas")} e {m} {(m == 1 ? "minuto" : "minutos")}");
                            }
                            else
                            {
                                Globals.H = 0;
                                Globals.V = -4;
                                Console.Clear();
                                draw();
                                Console.SetCursorPosition(38, 2);
                                Console.WriteLine("====== CONSULTA DE PROMOÇÃO ATIVA ======");
                                Console.SetCursorPosition(31 - Globals.H, 4);
                                Console.WriteLine("Nenhuma promoção ativa no momento.");
                            }
                            Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                            Console.WriteLine("Pressione ENTER para continuar...");
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 3:
                            Globals.H = 0;
                            Globals.V = 5;
                            Console.Clear();
                            draw();
                            Console.SetCursorPosition(42, 2);
                            Console.WriteLine("====== DESATIVAR PROMOÇÃO ======");

                            if (Globals.Pc != null)
                            {
                                double restante = (Globals.Tm - stop.ElapsedMilliseconds) / 3600000.0;
                                int h = (int)restante;
                                int m = (int)((restante - h) * 60);

                                Console.SetCursorPosition(31 - Globals.H, 4); Console.WriteLine("Promoção atual:");
                                Console.SetCursorPosition(31 - Globals.H, 5); Console.WriteLine($"Categoria: {Globals.Pc}");
                                Console.SetCursorPosition(31 - Globals.H, 6); Console.WriteLine($"Desconto: {Globals.Pp}%");
                                Console.SetCursorPosition(31 - Globals.H, 7); Console.WriteLine($"Tempo restante: {h} {(h == 1 ? "hora" : "horas")} e {m} {(m == 1 ? "minuto" : "minutos")}");

                                Console.SetCursorPosition(31 - Globals.H, 9);
                                Console.Write("Deseja realmente desativar esta promoção? (S/N): ");
                                Globals.Up = true;
                                string V = ReadLineLimited(1);
                                if (V == "S")
                                {
                                    Console.SetCursorPosition(31 - Globals.H, 11); Console.WriteLine("Motivo da desativação:");
                                    Console.SetCursorPosition(31 - Globals.H, 12); Console.WriteLine("1.Encerramento antecipado");
                                    Console.SetCursorPosition(31 - Globals.H, 13); Console.WriteLine("2.Erro na configuração");
                                    Console.SetCursorPosition(31 - Globals.H, 14); Console.WriteLine("3.Mudança de estratégia");
                                    Console.SetCursorPosition(31 - Globals.H, 15); Console.WriteLine("4.Outros");
                                    Console.SetCursorPosition(31 - Globals.H, 16); Console.Write("Escolha: ");
                                    if (int.TryParse(ReadLineLimited(1), out int op2) && op2 >= 1 && op2 <= 4)
                                    {
                                        Globals.Pc = null;
                                        Globals.Pp = 0;
                                        Globals.Tm = 0;
                                        stop.Stop();
                                        stop.Reset();

                                        Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                                        Console.WriteLine("Promoção desativada com sucesso!");
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.WriteLine("Código inválido.");
                                    }
                                }
                                else
                                {
                                    if (V != "N")
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.WriteLine("Código inválido.");
                                    }
                                    else
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                                        Console.WriteLine("Desativação cancelada.");
                                    }
                                }
                            }
                            else
                            {
                                Globals.H = 0;
                                Globals.V = -4;
                                Console.Clear();
                                draw();
                                Console.SetCursorPosition(42, 2);
                                Console.WriteLine("====== DESATIVAR PROMOÇÃO ======");
                                Console.SetCursorPosition(31 - Globals.H, 4);
                                Console.WriteLine("Nenhuma promoção ativa.");
                                Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                                Console.WriteLine("Pressione ENTER para continuar...");
                            }
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.SetCursorPosition(31 - Globals.H, 12);
                            Console.WriteLine("Opção inválida.");
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(31 - Globals.H, 12);
                    Console.WriteLine("Valor inválido.");
                    Globals.En = true;
                    ReadLineLimited(1);
                }
            }
        }
        static void seven()
        {
            bool exit = false;
            while (!exit)
            {
                Globals.H = 0;
                Globals.V = 0;
                Console.Clear();
                draw();
                Console.SetCursorPosition(43, 2);
                Console.WriteLine("======  RELATÓRIO DO DIA  ======");
                Console.SetCursorPosition(31 - Globals.H, 4);
                Console.WriteLine("1.Resumo de Vendas");
                Console.SetCursorPosition(31 - Globals.H, 6);
                Console.WriteLine("2.Faturamento Total");
                Console.SetCursorPosition(31 - Globals.H, 8);
                Console.WriteLine("3.Status do Estoque");
                Console.SetCursorPosition(31 - Globals.H, 10);
                Console.WriteLine("4.Voltar ao Menu Principal");
                Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                Console.Write("Escolha uma opção: ");

                if (int.TryParse(ReadLineLimited(1), out int op))
                {
                    switch (op)
                    {
                        case 1:
                            Globals.H = 0;
                            Globals.V = 0;
                            Console.Clear();
                            draw();

                            int Pv = 0;
                            Produto found = list.FirstOrDefault(p => p.Id == Globals.Vi);

                            Console.SetCursorPosition(39, 2);
                            Console.WriteLine("======  RESUMO DAS VENDAS DO DIA  ======");

                            if (Globals.Vn != 0)
                            {
                                foreach (var p in list)
                                {
                                    Pv += p.Vn;
                                }
                                Console.SetCursorPosition(31 - Globals.H, 4);
                                Console.WriteLine($"Total de vendas realizadas: {Globals.Vn}");
                                Console.SetCursorPosition(31 - Globals.H, 5);
                                Console.WriteLine($"Produtos vendidos: {Pv} {(Pv == 1 ? "unidade" : "unidades")}");
                                Console.SetCursorPosition(31 - Globals.H, 6);
                                Console.WriteLine($"Vendas com desconto promocional: {Globals.Vd}");
                                Console.SetCursorPosition(31 - Globals.H, 7);
                                Console.WriteLine($"Desconto total concedido: R${Globals.Dt}");
                                Console.SetCursorPosition(31 - Globals.H, 9);
                                Console.WriteLine($"Última venda: R${Globals.Vv} ({found.Name} - {found.Vn} {(found.Vn == 1 ? "unidade" : "unidades")})");
                                Console.SetCursorPosition(31 - Globals.H, 10);
                                Console.WriteLine($"Maior venda: R${Globals.Ma}");
                                Console.SetCursorPosition(31 - Globals.H, 11);
                                Console.WriteLine($"Menor venda: R${Globals.Me}");
                            }
                            else
                            {
                                Console.SetCursorPosition(31 - Globals.H, 4);
                                Console.WriteLine("Total de vendas realizadas: 0");
                                Console.SetCursorPosition(31 - Globals.H, 5);
                                Console.WriteLine("Produtos vendidos: 0 unidades");
                                Console.SetCursorPosition(31 - Globals.H, 6);
                                Console.WriteLine("Vendas com desconto promocional: 0");
                                Console.SetCursorPosition(31 - Globals.H, 7);
                                Console.WriteLine("Desconto total concedido: R$0 ");
                                Console.SetCursorPosition(31 - Globals.H, 9);
                                Console.WriteLine("Última venda: R$0");
                                Console.SetCursorPosition(31 - Globals.H, 10);
                                Console.WriteLine("Maior venda: R$0");
                                Console.SetCursorPosition(31 - Globals.H, 11);
                                Console.WriteLine("Menor venda: R$0");
                            }

                            Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                            Console.WriteLine("Pressione ENTER para continuar...");
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 2:
                            Globals.H = 0;
                            Globals.V = -1;
                            Console.Clear();
                            draw();
                            Console.SetCursorPosition(39, 2);
                            Console.WriteLine("======  FATURAMENTO TOTAL DO DIA  ======");
                            Console.SetCursorPosition(31 - Globals.H, 4);
                            Console.WriteLine($"Faturamento bruto: R${Globals.Fb}");
                            Console.SetCursorPosition(31 - Globals.H, 5);
                            Console.WriteLine($"Descontos aplicados: R${Globals.Dt}");
                            Console.SetCursorPosition(31 - Globals.H, 6);
                            Console.WriteLine($"Faturamento líquido: R${Globals.Fb - Globals.Dt}");
                            Console.SetCursorPosition(31 - Globals.H, 8);
                            Console.WriteLine($"Ticket médio: R${(Globals.Fb / Globals.Vn != double.PositiveInfinity ? "0" : $"{Globals.Fb / Globals.Vn}")}");
                            Console.SetCursorPosition(31 - Globals.H, 9);
                            Console.WriteLine("Meta do dia: R$ 1.000.00");
                            Console.SetCursorPosition(31 - Globals.H, 10);
                            if (Globals.Fb - Globals.Dt > 1000)
                            {
                                Console.WriteLine($"Status: Meta Atingida! (+{(Globals.Fb - Globals.Dt - 1000) / 10.0}%)");
                            }
                            else
                            {
                                Console.WriteLine($"Status: Meta Não Atingida ({(Globals.Fb - Globals.Dt - 1000) / 10.0}%)");
                            }
                            Console.SetCursorPosition(31 - Globals.H, 14 + Globals.V);
                            Console.WriteLine("Pressione ENTER para continuar...");
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 3:
                            Globals.H = 7;
                            Globals.V = 10;
                            int V = 0;
                            int N = 0;
                            int B = 0;
                            Console.Clear();
                            Console.SetCursorPosition(43, 2);
                            Console.WriteLine("======  STATUS - ESTOQUE  ======");
                            Console.SetCursorPosition(31 - Globals.H, 4);
                            Console.WriteLine("Verificando todos os produtos cadastrados...");

                            if (list.Count != 0)
                            {
                                Console.SetCursorPosition(31 - Globals.H, 11);
                                Console.WriteLine("Produtos com estoque normal: ");
                                Console.SetCursorPosition(31 - Globals.H, 12);
                                Console.WriteLine("Código:      ||      Nome:      ||      Estoque:      ||    Status: ");
                                foreach (var p in list)
                                {
                                    if (p.Qe >= 5)
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 13 + V);
                                        Console.WriteLine(p.Id);

                                        Console.SetCursorPosition(54 - Globals.H - (p.Name.Length / 2), 13 + V);
                                        Console.WriteLine(p.Name);

                                        Console.SetCursorPosition(71 - Globals.H, 13 + V);
                                        Console.WriteLine($"{p.Qe} {(p.Qe == 1 ? "unidade" : "unidades")}");

                                        Console.SetCursorPosition(91 - Globals.H, 13 + V);
                                        Console.WriteLine("Normal");

                                        N++;
                                        V++;
                                    }
                                }
                                Console.SetCursorPosition(31 - Globals.H, 14 + V);
                                Console.WriteLine("Produtos com estoque baixo: ");
                                Console.SetCursorPosition(31 - Globals.H, 15 + V);
                                Console.WriteLine("Código:      ||      Nome:      ||      Estoque:      ||    Status: ");
                                foreach (var p in list)
                                {
                                    if (p.Qe < 5)
                                    {
                                        Console.SetCursorPosition(31 - Globals.H, 16 + V);
                                        Console.WriteLine(p.Id);

                                        Console.SetCursorPosition(54 - Globals.H - (p.Name.Length / 2), 16 + V);
                                        Console.WriteLine(p.Name);

                                        Console.SetCursorPosition(71 - Globals.H, 16 + V);
                                        Console.WriteLine($"{p.Qe} {(p.Qe == 1 ? "unidade" : "unidades")}");

                                        Console.SetCursorPosition(91 - Globals.H, 16 + V);
                                        Console.WriteLine($"{(p.Qe <= 2 ? "Crítico" : "Atenção")}");

                                        B++;
                                        V++;
                                    }
                                }
                                Console.SetCursorPosition(31 - Globals.H, 6);
                                Console.WriteLine("Resumo geral do estoque:");
                                Console.SetCursorPosition(31 - Globals.H, 7);
                                Console.WriteLine($"Produtos cadastrados: {V}");
                                Console.SetCursorPosition(31 - Globals.H, 8);
                                Console.WriteLine($"Produtos com estoque normal: {N}");
                                Console.SetCursorPosition(31 - Globals.H, 9);
                                Console.WriteLine($"Produtos com estoque baixo: {B}");

                                Globals.V += V;
                                Console.SetCursorPosition(31 - Globals.H, 7 + Globals.V);
                                Console.WriteLine($"Valor");
                                Console.SetCursorPosition(31 - Globals.H, 7 + Globals.V);
                                Console.WriteLine($"Ação necessária: Reabastecer {B} {(B == 1 ? "produto" : "produtos")}");
                            }
                            else
                            {
                                Globals.V = 0;
                                Console.SetCursorPosition(31 - Globals.H, 7);
                                Console.WriteLine("Nenhum produto cadastrado.");
                            }
                            Console.SetCursorPosition(31 - Globals.H, 9 + Globals.V);
                            Console.WriteLine("Pressione ENTER para continuar...");
                            Globals.V -= 5;
                            draw();
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                            Console.WriteLine("Opção inválida.");
                            Globals.En = true;
                            ReadLineLimited(1);
                            break;
                    }
                }
                else
                {
                    Console.SetCursorPosition(31 - Globals.H, 12 + Globals.V);
                    Console.WriteLine("Valor inválido.");
                    Globals.En = true;
                    ReadLineLimited(1);
                }
            }
        }
        static void setProducts()
        {
            Produto P1 = new Produto();
            Produto P2 = new Produto();
            Produto P3 = new Produto();
            Produto P4 = new Produto();

            P1.Id = 101;
            P1.Name = "Teclado RGB";
            P1.Ctl = "Informática";
            P1.Pc = 200;
            P1.Ml = 15;
            P1.Qe = 2;
            P1.Vn = 0;

            P2.Id = 102;
            P2.Name = "Relógio Analógico";
            P2.Ctl = "Acessórios";
            P2.Pc = 130;
            P2.Ml = 20;
            P2.Qe = 15;
            P2.Vn = 0;

            P3.Id = 103;
            P3.Name = "Caderno Capa Dura";
            P3.Ctl = "Papelaria";
            P3.Pc = 45;
            P3.Ml = 10;
            P3.Qe = 4;
            P3.Vn = 0;

            P4.Id = 104;
            P4.Name = "Fone Bluetooth";
            P4.Ctl = "Telefonia";
            P4.Pc = 120;
            P4.Ml = 18;
            P4.Qe = 10;
            P4.Vn = 0;

            list.Add(P1);
            list.Add(P2);
            list.Add(P3);
            list.Add(P4);
        }

        public class Produto
        {
            public int Id { get; set; } //Código do produto
            public string Name { get; set; } //Nome
            public string Ctl { get; set; } //Categoria
            public int Pc { get; set; } //Preço de custo
            public int Ml { get; set; } //Margem de lucro
            public int Qe { get; set; } //Quantidade em estoque
            public int Vn { get; set; } //Vendas
        }

        public static class Globals
        {
            public static int H { get; set; } //Horizontal
            public static int V { get; set; } //Vertical
            public static int Vn { get; set; } //vendas
            public static int Vd { get; set; } //vendas com desconto
            public static double Dt { get; set; } //desconto total
            public static double Vv { get; set; } //ultima venda (Valor)
            public static int Vi { get; set; } //ultima venda (id)
            public static double Ma { get; set; } //maior venda
            public static double Me { get; set; } //menor venda
            public static double Fb { get; set; } //faturamento bruto
            public static int Pp { get; set; } //Promoção porcentagem
            public static string Pc { get; set; } //Promoção categoria
            public static int Tm { get; set; } //Tempo
            public static bool Up { get; set; } //Maiúsculo
            public static bool En { get; set; } //Enter
        }
    }
}