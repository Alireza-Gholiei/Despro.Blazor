namespace Despro.Blazor.Base.BaseGenerals
{
    public class ClassBuilder
    {
        public List<string> ClassNames = new();

        public ClassBuilder(string classNames = null)
        {
            try
            {
                _ = Add(classNames);
            }
            catch (Exception)
            {

            }
        }

        public ClassBuilder Add(string className)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(className))
                {
                    List<string> classNames = className
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Distinct()
                        .ToList();

                    foreach (string name in classNames.Where(name =>
                        !string.IsNullOrWhiteSpace(name) && !ClassNames.Contains(name)))
                        ClassNames.Add(name);
                }

                return this;
            }
            catch (Exception)
            {
                return this;
            }
        }

        public ClassBuilder AddIf(string className, bool isOk)
        {
            try
            {
                return isOk ? Add(className) : this;
            }
            catch (Exception)
            {
                return this;
            }
        }

        public ClassBuilder AddCompare<T>(T compare, Dictionary<T, string> with)
        {
            try
            {
                foreach (KeyValuePair<T, string> kvp in with) _ = AddCompare(kvp.Value, compare, kvp.Key);

                return this;
            }
            catch (Exception)
            {
                return this;
            }
        }

        public ClassBuilder AddCompare<T>(string className, T compare, T with)
        {
            try
            {
                return AddIf(className, compare.Equals(with));
            }
            catch (Exception)
            {
                return this;
            }
        }

        public ClassBuilder Remove(string className)
        {
            try
            {
                _ = ClassNames.RemoveAll(c => c.Equals(className, StringComparison.InvariantCultureIgnoreCase));
                return this;
            }
            catch (Exception)
            {
                return this;
            }
        }

        public override string ToString()
        {
            try
            {
                string? result = ClassNames == null || !ClassNames.Any()
                    ? null
                        : string.Join(" ", ClassNames.Distinct().Where(c => !string.IsNullOrWhiteSpace(c)));

                return string.IsNullOrWhiteSpace(result) ? "" : result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}