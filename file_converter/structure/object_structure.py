from typing import List
from elements.file import File
from visitors.file_visitor import FileVisitor

class ObjectStructure:
    def __init__(self):
        self._elements: List[File] = []
    
    def add_file(self, file: File) -> None:
        self._elements.append(file)
        print(f"Добавлен файл: {file.get_name()}")
    
    def remove_file(self, file: File) -> None:
        if file in self._elements:
            self._elements.remove(file)
            print(f"Удален файл: {file.get_name()}")
    
    def accept(self, visitor: FileVisitor) -> None:
        for element in self._elements:
            element.accept(visitor)
    
    def get_files(self) -> List[File]:
        return self._elements
    
    def clear(self) -> None:
        self._elements.clear()
        print("Все файлы удалены")