using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puppy.Engine.SaveData {
    public class CloudFileWriter : IFileWriter {
        public string Path => throw new NotImplementedException();

        public bool Exits() {
            throw new NotImplementedException();
        }

        public void Write(string data, Action onCompleted, Action onFailed) {
            throw new NotImplementedException();
        }
    }
}
