from typing import TYPE_CHECKING
from .file import File

if TYPE_CHECKING:
    from visitors.file_visitor import FileVisitor

class DocumentFile(File):
    def __init__(self, path: str, pages: int, word_count: int):
        super().__init__(path)
        self._pages = pages
        self._word_count = word_count
    
    @property
    def pages(self) -> int:
        return self._pages
    
    @property
    def word_count(self) -> int:
        return self._word_count
    
    def accept(self, visitor: 'FileVisitor') -> None:
        visitor.visit_document_file(self)
    
    def get_name(self) -> str:
        return f"Document: {self._path}"
    
    def get_size(self) -> int:
        return self._word_count * 5