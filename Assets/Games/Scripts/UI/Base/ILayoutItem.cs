﻿using System.Collections.Generic;
public interface IItem<T> {
    T dataStack { get; set; }
    IItem<T> Generate();
}

public interface ILayout<U, T> where U : IItem<T> {
    List<U> Items { get; set; }
    void GenerateItem();
}
