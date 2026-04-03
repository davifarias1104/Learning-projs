/*
* Participantes:
* Davi Farias de Andrade
* Vitor Hugo Luz Pereira
* Arthur Jesus Nascimento
* Davi Andrade dos Santos Rodrigues Oliveira
* 
* Site onde foi tirado os dados:
* https://tempo.inmet.gov.br/TabelaEstacoes
* São Paulo mês de Abril
*/

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;


internal class Program
{
    // Temp. máx (ºC), temp min (ºC), umidade relativa do ar (%), precipitação (mm)
    static double[,] DadosMes = {
            {30.1, 19.4, 77, 17.4}, {31.8, 21.3, 68, 0}, {28.7, 21.6, 75, 4.7}, {27.3, 20.6, 88, 22.3}, {20.8, 15.8, 87, 12.2}, {22.1, 15.4, 75, 0.7}, {23.7, 15.8, 74, 0}, {28.6, 16, 73, 0}, {29.5, 17.6, 72, 0}, {26.3, 17.4, 90, 1.2}, {27.4, 17.2, 79, 25.7}, {28, 18.3, 76, 0}, {29.5, 18.3, 71, 0}, {23.1, 18.5, 84, 59.8}, {27.6, 16.1, 76, 0}, {27.1, 18.6, 76, 0.8}, {27.6, 18.6, 73, 0}, {28.1, 19.1, 76, 0}, {28.1, 18.8, 77, 11}, {23.3, 18.8, 84, 6.4}, {23.7, 17.3, 83, 0}, {23.3, 15.4, 79, 0}, {25.7, 17, 76, 0}, {26.9, 18.4, 81, 0}, {22.7, 18.2, 95, 13.4}, {26.5, 18.3, 79, 16.4}, {27.3, 18.8, 80, 0}, {25.4, 19.4, 83, 0}, {23.3, 17.4, 80, 0.4}, {24.1, 14.4, 72, 0}
        };
    public static void Main()
    {
        sbyte H = 0, V = 2;

        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        while (true)
        {
            Console.Clear();
            draw(H, V);

            Console.SetCursorPosition(42, 2);
            Console.WriteLine("======  Dados Climáticos  ======");

            Console.SetCursorPosition(32 - H, 4);
            Console.WriteLine("1.Médias do Mês");

            Console.SetCursorPosition(32 - H, 6);
            Console.WriteLine("2.Médias Semanais");

            Console.SetCursorPosition(32 - H, 8);
            Console.WriteLine("3.Lista de Temperatura Máxima");

            Console.SetCursorPosition(32 - H, 10);
            Console.WriteLine("4.Pesquisar");

            Console.SetCursorPosition(32 - H, 12);
            Console.WriteLine("5.Análise Quinzenal");

            Console.SetCursorPosition(32 - H, 14);
            Console.WriteLine("6.Sair");

            Console.SetCursorPosition(32 - H, 14 + V);
            Console.Write("Escolha uma opção: ");

            if (sbyte.TryParse(ReadLineLimited(1), out sbyte op))
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
                        return;
                }
            }
        }
    }

    static void draw(sbyte H, sbyte V)
    {
        char cSED = '\u2554'; // ╔
        char cSDD = '\u2557'; // ╗
        char cIED = '\u255A'; // ╚
        char cIDD = '\u255D'; // ╝
        char bHD = '\u2550';  // ═
        char bVD = '\u2551';  // ║
        char cVED = '\u2560'; // ╠
        char cVDD = '\u2563'; // ╣

        Console.SetCursorPosition(30 - H, 1);
        Console.Write(cSED);
        Console.SetCursorPosition(30 - H, 15 + V);
        Console.Write(cIED);
        Console.SetCursorPosition(87 + H, 1);
        Console.Write(cSDD);
        Console.SetCursorPosition(87 + H, 15 + V);
        Console.Write(cIDD);

        for (int i = 0; i <= 55 + H * 2; i++)
        {
            Console.SetCursorPosition(31 + i - H, 1);
            Console.Write(bHD);
            Console.SetCursorPosition(31 + i - H, 3);
            Console.Write(bHD);
            Console.SetCursorPosition(31 + i - H, 13 + V);
            Console.Write(bHD);
            Console.SetCursorPosition(31 + i - H, 15 + V);
            Console.Write(bHD);
        }

        for (int i = 0 - V; i <= 12; i++)
        {
            if (i == 1 - V ^ i == 11)
            {
                Console.SetCursorPosition(30 - H, 2 + i + V);
                Console.Write(cVED);
                Console.SetCursorPosition(87 + H, 2 + i + V);
                Console.Write(cVDD);
                i++;
            }
            Console.SetCursorPosition(30 - H, 2 + i + V);
            Console.Write(bVD);
            Console.SetCursorPosition(87 + H, 2 + i + V);
            Console.Write(bVD);
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
                char upperChar = key.KeyChar;
                input.Append(upperChar);
                Console.Write(upperChar);
            }
        }
        return input.ToString();
    }

    static void one()
    {
        sbyte H = 0, V = 7;
        int n = DadosMes.GetLength(0);
        double somaMax = 0, somaMin = 0, somaU = 0, somaP = 0;
        double max = 0, min = 100, maxP = 0;
        int dMax = 0, dMin = 0, dP = 0;
        int chuva20 = 0, umid60 = 0, semChuva = 0;
        double varM = 0, varN = 0; int dVar = 0;
        double somaChuva = 0; int diasChuva = 0;

        Console.Clear();
        draw(H, V);

        for (int i = 0; i < n; i++)
        {
            somaMax += DadosMes[i, 0];
            somaMin += DadosMes[i, 1];
            somaU += DadosMes[i, 2];
            somaP += DadosMes[i, 3];

            if (DadosMes[i, 0] > max)
            {
                max = DadosMes[i, 0];
                dMax = i + 1;
            }
            if (DadosMes[i, 1] < min)
            {
                min = DadosMes[i, 1];
                dMin = i + 1;
            }
            if (DadosMes[i, 3] > maxP)
            {
                maxP = DadosMes[i, 3];
                dP = i + 1;
            }

            if (DadosMes[i, 3] > 20)
                chuva20++;

            if (DadosMes[i, 2] < 60)
                umid60++;

            if (DadosMes[i, 3] == 0)
                semChuva++;

            if (DadosMes[i, 0] - DadosMes[i, 1] > varM - varN)
            {
                varM = DadosMes[i, 0];
                varN = DadosMes[i, 1];
                dVar = i + 1;
            }

            if (DadosMes[i, 3] > 0)
            {
                somaChuva += DadosMes[i, 3];
                diasChuva++;
            }
        }

        Console.SetCursorPosition(40, 2);
        Console.WriteLine("====== Análise Climática do Mês ======");

        Console.SetCursorPosition(32 - H, 4);
        Console.WriteLine($"Médias:");
        Console.SetCursorPosition(32 - H, 5);
        Console.WriteLine($"Temp. Máx: {somaMax / n,1:f1} °C");
        Console.SetCursorPosition(32 - H, 6);
        Console.WriteLine($"Temp. Mín: {somaMin / n,1:f1} °C");
        Console.SetCursorPosition(32 - H, 7);
        Console.WriteLine($"Umidade: {somaU / n,1:f1} %");
        Console.SetCursorPosition(32 - H, 8);
        Console.WriteLine($"Precipitação: {somaP / n,1:f1} mm");

        Console.SetCursorPosition(32 - H, 10);
        Console.WriteLine($"Dia mais quente: Dia {dMax} ({max} °C)");
        Console.SetCursorPosition(32 - H, 11);
        Console.WriteLine($"Dia mais frio: Dia {dMin} ({min} °C)");
        Console.SetCursorPosition(32 - H, 12);
        Console.WriteLine($"Dia mais chuvoso: Dia {dP} ({maxP} mm)");

        Console.SetCursorPosition(32 - H, 14);
        Console.WriteLine($"Dias com chuva > 20mm: {chuva20}");
        Console.SetCursorPosition(32 - H, 15);
        Console.WriteLine($"Dias com umidade < 60%: {umid60}");
        Console.SetCursorPosition(32 - H, 16);
        Console.WriteLine($"Dias sem chuva: {semChuva}");
        Console.SetCursorPosition(32 - H, 17);
        Console.WriteLine($"Maior variação térmica: Dia {dVar} ({varM} °C - {varN} °C)");
        Console.SetCursorPosition(32 - H, 18);
        Console.WriteLine($"Média de chuva nos dias chuvosos: {somaChuva / diasChuva,1:f1} mm");

        Console.SetCursorPosition(32 - H, 14 + V);
        Console.WriteLine("Pressione ENTER para continuar...");

        ReadLineLimited(0);
    }

    static void two()
    {
        sbyte H = 1, V = -1;
        double[] Mt = new double[5];
        double[] Mc = new double[5];
        int Semana = 7;

        Console.Clear();
        draw(H, V);

        Console.SetCursorPosition(39, 2);
        Console.WriteLine("====== Análise Climática Semanal ======");

        for (int s = 0; s < 5; s++)
        {
            int dias = (s < 4) ? 7 : 2;
            double somaT = 0, somaC = 0;
            for (int i = 0; i < dias; i++)
            {
                int index = s * Semana + i;
                if (index >= DadosMes.GetLength(0))
                {
                    break;
                }
                somaT += DadosMes[index, 0];
                somaC += DadosMes[index, 3];
            }
            Mt[s] = somaT / dias;
            Mc[s] = somaC / dias;
        }

        int Sq = Array.IndexOf(Mt, Mt.Max()) + 1;
        int Sc = Array.IndexOf(Mc, Mc.Max()) + 1;

        Console.SetCursorPosition(32 - H, 4);
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Semana {i + 1}: Média Temp. = {Mt[i]:F1} °C | Média Chuva = {Mc[i]:F1} mm");
            Console.SetCursorPosition(32 - H, 5 + i);
        }

        Console.SetCursorPosition(32 - H, 11 + V);
        Console.WriteLine($"Semana mais quente: Semana {Sq} ({Mt[Sq - 1]:F1} °C)");
        Console.SetCursorPosition(32 - H, 12 + V);
        Console.WriteLine($"Semana mais chuvosa: Semana {Sc} ({Mc[Sc - 1]:F1} mm)");

        Console.SetCursorPosition(32 - H, 14 + V);
        Console.WriteLine("Pressione ENTER para continuar...");

        ReadLineLimited(0);
    }

    static void three()
    {
        sbyte H = 15, V = 7;
        Console.Clear();
        draw(H, V);
        Console.SetCursorPosition(37, 2);
        Console.WriteLine("====== Temperaturas Máximas Ordenadas ======");

        int n = DadosMes.GetLength(0);
        double[,] sorted = (double[,])DadosMes.Clone();
        int[] dias = new int[n];

        for (int i = 0; i < n; i++)
            dias[i] = i + 1;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (sorted[j, 0] > sorted[j + 1, 0])
                {
                    for (int k = 0; k < 4; k++)
                    {
                        double temp = sorted[j, k];
                        sorted[j, k] = sorted[j + 1, k];
                        sorted[j + 1, k] = temp;
                    }
                    int tempDia = dias[j];
                    dias[j] = dias[j + 1];
                    dias[j + 1] = tempDia;
                }
            }
        }

        Console.SetCursorPosition(31 - H, 4);
        Console.WriteLine("Dia |   Máx  |   Mín  |  Umd |   Precip   ||  Dia |   Máx  |   Mín  |  Umd |   Precip");

        for (int i = 0; i < n; i++)
        {
            int Y = (i >= 15) ? 5 + (i - 15) : 5 + i;
            int X = (i >= 15) ? 62 : 31 - H;

            Console.SetCursorPosition(X, Y);
            Console.Write($" {dias[i],2} | {sorted[i, 0],6:F1} | {sorted[i, 1],6:F1} | {sorted[i, 2],3:F0}% | {sorted[i, 3],6:F1}mm");
        }

        Console.SetCursorPosition(32 - H, 14 + V);
        Console.WriteLine("Pressione ENTER para continuar...");

        ReadLineLimited(0);
    }


    static void four()
    {
        sbyte H = 0, V = -5;
        Console.Clear();
        draw(H, V);
        Console.SetCursorPosition(39, 2);
        Console.WriteLine("====== Pesquisa por Precipitação ======");

        Console.SetCursorPosition(32 - H, 4);
        Console.Write("Digite a quantidade exata de chuva (mm): ");
        if (double.TryParse(Console.ReadLine(), out double valorBusca))
        {
            bool encontrado = false;
            for (int i = 0; i < DadosMes.GetLength(0); i++)
            {
                if (DadosMes[i, 3] == valorBusca)
                {
                    Console.SetCursorPosition(32, 12 + V);
                    Console.WriteLine($"Dia {i + 1}: {DadosMes[i, 0]}°C - {DadosMes[i, 1]}°C  {DadosMes[i, 2]}%  {DadosMes[i, 3]} mm");
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                Console.SetCursorPosition(32 - H, 12 + V);
                Console.WriteLine("Nenhum dia com essa quantidade exata de chuva.");
            }
        }
        else
        {
            Console.SetCursorPosition(32 - H, 12 + V);
            Console.WriteLine("Valor inválido.");
        }

        Console.SetCursorPosition(32 - H, 14 + V);
        Console.WriteLine("Pressione ENTER para continuar...");

        ReadLineLimited(0);
    }

    static void five()
    {
        sbyte H = 8, V = -2;
        Console.Clear();
        draw(H, V);

        int linhas = DadosMes.GetLength(0);
        int colunas = DadosMes.GetLength(1);

        for (int i = 0; i < linhas; i++)
        {
            double tmax = DadosMes[i, 0];
            double tmin = DadosMes[i, 1];
            double umid = DadosMes[i, 2];
            double prec = DadosMes[i, 3];

            if (tmax < -10 || tmax > 50 ||
                tmin < -20 || tmin > 40 ||
                umid < 0 || umid > 100 ||
                prec < 0 || prec > 500 ||
                tmax < tmin)
            {
                Console.SetCursorPosition(32 - H, 4);
                Console.WriteLine("Validação de Dados: Valores Inválidos");
                Console.ReadKey();
                return;
            }
        }

        int diasQuinzena1 = linhas / 2;
        int diasQuinzena2 = linhas - diasQuinzena1;

        double somaTempMaxQ1 = 0;
        double somaTempMaxQ2 = 0;
        double somaChuvaQ1 = 0;
        double somaChuvaQ2 = 0;
        int diasConforto = 0;

        for (int i = 0; i < linhas; i++)
        {
            double tempMax = DadosMes[i, 0];
            double tempMin = DadosMes[i, 1];
            double chuva = DadosMes[i, 3];

            double mediaTemp = (tempMax + tempMin) / 2;
            if (mediaTemp >= 22 && mediaTemp <= 28)
                diasConforto++;

            if (i < diasQuinzena1)
            {
                somaTempMaxQ1 += tempMax;
                somaChuvaQ1 += chuva;
            }
            else
            {
                somaTempMaxQ2 += tempMax;
                somaChuvaQ2 += chuva;
            }
        }

        double mediaTempMaxQ1 = somaTempMaxQ1 / diasQuinzena1;
        double mediaTempMaxQ2 = somaTempMaxQ2 / diasQuinzena2;

        double difTemp = mediaTempMaxQ2 - mediaTempMaxQ1;
        double difChuva = somaChuvaQ2 - somaChuvaQ1;
        double percConforto = (double)diasConforto / linhas * 100;

        string tendenciaTemp = difTemp > 0
            ? $"Aquecimento (+{difTemp:F1}°C da 1ª para 2ª quinzena)"
            : $"Resfriamento ({difTemp:F1}°C da 1ª para 2ª quinzena)";

        string tendenciaChuva = difChuva < 0
            ? $"Redução ({difChuva:F0}mm da 1ª para 2ª quinzena)"
            : $"Aumento (+{difChuva:F0}mm da 1ª para 2ª quinzena)";

        string pastaDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string caminhoArquivo = Path.Combine(pastaDesktop, "relatorio_climatico.txt");

        string conteudo = $"Tendência de Temperatura: {tendenciaTemp}\n" +
                          $"Tendência de Chuva: {tendenciaChuva}\n" +
                          $"Índice de Conforto Térmico: {diasConforto} dias ({percConforto:F0}%) com condições ideais\n";

        File.WriteAllText(caminhoArquivo, conteudo);


        Console.SetCursorPosition(37 + H, 2);
        Console.WriteLine("====== Análise Quinzenal ======");
        Console.SetCursorPosition(32 - H, 4);
        Console.WriteLine($"Validação de Dados: Todos os dados estão consistentes!");
        Console.SetCursorPosition(32 - H, 6);
        Console.WriteLine($"Tendência de Temperatura: {tendenciaTemp}");
        Console.SetCursorPosition(32 - H, 7);
        Console.WriteLine($"Tendência de Chuva: {tendenciaChuva}");
        Console.SetCursorPosition(32 - H, 8);
        Console.WriteLine($"Índice de Conforto Térmico: {diasConforto} dias ({percConforto:F0}%) com condições ideais");
        Console.SetCursorPosition(32 - H, 16 + V);
        Console.WriteLine($"Relatório completo salvo em: {caminhoArquivo}");

        Console.SetCursorPosition(32 - H, 14 + V);
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadKey();
    }
}
