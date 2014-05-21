using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project.Domain
{
    public class TranslitWithXMLOptions : IDisposable
    {
        private bool disposed = false;

        //словари правил транслитерации
        private Dictionary<char, string> OrdinaryRules;
        private Dictionary<char, string> FirstLetterRules;
        private Dictionary<string, string> SpecialRules;

        private StringBuilder Sb;

        /// <summary>
        /// Конструктор получающий правила транслитерации из XML документа в App.config
        /// </summary>
        public TranslitWithXMLOptions()
        {
            try
            {
                XDocument doc = XDocument.Load(ConfigurationSettings.AppSettings["XMLdoc"].ToString());
                this.FirstLetterRules = SetUpDictionary(doc, "FirstLetterRule");
                this.OrdinaryRules = SetUpDictionary(doc, "OrdinaryRule");
                this.SpecialRules = SetUpDictionarySpecial(doc, "SpecialRules");
            }
            catch (NullReferenceException)
            {
                this.FirstLetterRules = new Dictionary<char, string>();
                this.OrdinaryRules = new Dictionary<char, string>();
                this.SpecialRules = new Dictionary<string, string>();

                BackUpDictionaryInitialize();
            }
            catch (System.IO.FileNotFoundException)
            {
                this.FirstLetterRules = new Dictionary<char, string>();
                this.OrdinaryRules = new Dictionary<char, string>();
                this.SpecialRules = new Dictionary<string, string>();

                BackUpDictionaryInitialize();

            }
        }

      

        /// <summary>
        /// Основной метод транслитерации
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Translit(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new EmptyInputException();
            }

            Sb = new StringBuilder();
            Sb.Append(text);

            SpicialRulesTranslit();

            var words = Sb.ToString().Split(' ');
            Sb.Clear();

            for (int i = 0; i < words.Length; i++)
            {
                OrdinaryTranslit(words[i]);

                if (i != words.Length - 1)
                {
                    Sb.Append(' ');
                }
            }

            return Sb.ToString();
        }


        #region TranslitMethods

        /// <summary>
        /// Транслитерация по специальным правилам (зг -> zgh)
        /// </summary>
        private void SpicialRulesTranslit()
        {

            foreach (var rule in SpecialRules)
            {
                Sb.Replace(rule.Key, rule.Value);
            }
        }

        /// <summary>
        /// Мето транслитерации стандартного правила транслитерации
        /// </summary>
        /// <param name="word"></param>
        private void OrdinaryTranslit(string word)
        {
            if (String.IsNullOrEmpty(word))
            {
                return;
            }

            FirstLetterRule(word);

            for (int i = 1; i < word.Length; i++)
            {
                try
                {
                    Sb.Append(OrdinaryRules[word[i]]);
                }
                catch (KeyNotFoundException)
                {

                    Sb.Append(word[i]);
                }
            }
        }

        /// <summary>
        /// Метод транлитерации первых букв 
        /// </summary>
        /// <param name="word"></param>
        private void FirstLetterRule(string word)
        {

            try
            {
                Sb.Append(FirstLetterRules[word[0]]);
            }
            catch (KeyNotFoundException)
            {
                try
                {
                    Sb.Append(OrdinaryRules[word[0]]);
                }
                catch (KeyNotFoundException)
                {

                    Sb.Append(word[0]);
                }

            }
        }

        #endregion

        #region SetUpMethods

        /// <summary>
        /// Метод заполнения словарей обычных правил и правил первой буквы
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodename"></param>
        /// <returns></returns>
        private Dictionary<char, string> SetUpDictionary(XDocument doc, string nodename)
        {
            Dictionary<char, string> dict = new Dictionary<char, string>();

            var q = doc.Document.Root.Elements().Where(x => x.Name == nodename);


            foreach (var item in q.Elements())
            {
                dict.Add((char)item.Attribute("letter").Value[0], (string)item.Value.ToString());
            }

            return dict;
        }

        /// <summary>
        /// Метод заполнения словаря специальных правил 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodename"></param>
        /// <returns></returns>
        private Dictionary<string, string> SetUpDictionarySpecial(XDocument doc, string nodename)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            var q = doc.Document.Root.Elements().Where(x => x.Name == nodename);

            foreach (var item in q.Elements())
            {
                dict.Add((string)item.Attribute("letter").Value.ToString(), (string)item.Value.ToString());
            }

            return dict;
        }

        /// <summary>
        /// Метод запоняющий все словари в случаи отсутствия XML документа
        /// </summary>
        private void BackUpDictionaryInitialize()
        {
            OrdinaryRules.Add('А', "A");
            OrdinaryRules.Add('а', "a");

            OrdinaryRules.Add('Б', "B");
            OrdinaryRules.Add('б', "b");

            OrdinaryRules.Add('В', "V");
            OrdinaryRules.Add('в', "v");

            OrdinaryRules.Add('Г', "H");
            OrdinaryRules.Add('г', "h");

            OrdinaryRules.Add('Ґ', "G");
            OrdinaryRules.Add('ґ', "g");

            OrdinaryRules.Add('Д', "D");
            OrdinaryRules.Add('д', "d");

            OrdinaryRules.Add('Е', "E");
            OrdinaryRules.Add('е', "e");

            OrdinaryRules.Add('є', "ie");

            OrdinaryRules.Add('Ж', "Zh");
            OrdinaryRules.Add('ж', "zh");

            OrdinaryRules.Add('З', "Z");
            OrdinaryRules.Add('з', "z");

            OrdinaryRules.Add('И', "Y");
            OrdinaryRules.Add('и', "y");

            OrdinaryRules.Add('І', "I");
            OrdinaryRules.Add('і', "i");

            OrdinaryRules.Add('ї', "i");

            OrdinaryRules.Add('й', "i");

            OrdinaryRules.Add('К', "K");
            OrdinaryRules.Add('к', "k");

            OrdinaryRules.Add('Л', "L");
            OrdinaryRules.Add('л', "l");

            OrdinaryRules.Add('М', "M");
            OrdinaryRules.Add('м', "m");

            OrdinaryRules.Add('Н', "N");
            OrdinaryRules.Add('н', "n");

            OrdinaryRules.Add('О', "O");
            OrdinaryRules.Add('о', "o");

            OrdinaryRules.Add('П', "P");
            OrdinaryRules.Add('п', "p");

            OrdinaryRules.Add('Р', "R");
            OrdinaryRules.Add('р', "r");

            OrdinaryRules.Add('С', "S");
            OrdinaryRules.Add('с', "s");

            OrdinaryRules.Add('Т', "T");
            OrdinaryRules.Add('т', "t");

            OrdinaryRules.Add('У', "U");
            OrdinaryRules.Add('у', "u");

            OrdinaryRules.Add('Ф', "F");
            OrdinaryRules.Add('ф', "f");

            OrdinaryRules.Add('Х', "Kh");
            OrdinaryRules.Add('х', "kh");

            OrdinaryRules.Add('Ц', "Ts");
            OrdinaryRules.Add('ц', "ts");

            OrdinaryRules.Add('Ч', "Ch");
            OrdinaryRules.Add('ч', "ch");

            OrdinaryRules.Add('Ш', "Sh");
            OrdinaryRules.Add('ш', "sh");

            OrdinaryRules.Add('Щ', "Shch");
            OrdinaryRules.Add('щ', "shch");

            OrdinaryRules.Add('ю', "iu");

            OrdinaryRules.Add('я', "ia");

            OrdinaryRules.Add('\'', string.Empty);
            OrdinaryRules.Add('ь', string.Empty);

            FirstLetterRules.Add('Ї', "Yi");
            FirstLetterRules.Add('ї', "yi");

            FirstLetterRules.Add('Є', "Ye");
            FirstLetterRules.Add('є', "ye");

            FirstLetterRules.Add('Й', "Y");
            FirstLetterRules.Add('й', "y");

            FirstLetterRules.Add('Ю', "Yu");
            FirstLetterRules.Add('ю', "yu");

            FirstLetterRules.Add('Я', "Ya");
            FirstLetterRules.Add('я', "ya");

            SpecialRules.Add("Зг", "Zgh");
            SpecialRules.Add("зг", "zgh");
        }

        #endregion


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.FirstLetterRules = null; ;
                    this.OrdinaryRules = null;
                    this.SpecialRules = null;
                    if (this.Sb != null)
                    {
                        this.Sb.Clear();
                    }

                }

                disposed = true;
            }
        }

    }
}
