namespace SD_HW2.Category;

public static class CategoryRepository
{
    private static CategoryFactory _categoryFactory = new CategoryFactory();
    public static List<Category> Categories { get; } = [];
    
    public static List<String> CategoriesNames
    {
        get
        {
            List<String> categoriesNames = [];
            categoriesNames.AddRange(Categories.Select(category => category.Name));

            return categoriesNames;
        }
    }

    public static List<String> CategoriesByType(string typeStr)
    {
        List<String> categoriesNames = [];

        var type = typeStr == "Расход" ? Type.Withdrawal : Type.Deposit;
        
        foreach (var category in Categories)
        {
            if (category.Type == type)
            {
                categoriesNames.Add(category.Name);
            }
        }
        
        return categoriesNames;
    }
    
    public static void AddCategory(Type type, string name)
    {
        var category = _categoryFactory.CreateCategory(type, name);
        Categories.Add(category);
    }

    public static void RemoveCategory(string name)
    {
        bool found = false;
        foreach (var category in Categories)
        {
            if (category.Name == name)
            {
                Categories.Remove(category);
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new ArgumentOutOfRangeException($"Не существует категории с именем {name} ");
        }
    }
    
    public static Category FindCategory(string name)
    {
        foreach (var category in Categories) 
        {
            if (category.Name == name)
            {
                return category;
            }
        }
        throw new ArgumentOutOfRangeException($"Не существует категории с именем {name}");
    }
    
    public static void ChangeCategoryName(string name, string newName)
    {
        var category = FindCategory(name);
        category.Name = newName;
    }
    
    public static void ChangeCategoryType(string name, Type newType)
    {
        var category = FindCategory(name);
        category.Type = newType;
    }
}