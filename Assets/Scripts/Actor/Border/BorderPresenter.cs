using System;

public class BorderPresenter : IBorderPresenter
{
    private readonly IBorder Model;

    public BorderPresenter(IBorder model)
    {
        Model = model ?? throw new ArgumentNullException(nameof(model));
    }
}
