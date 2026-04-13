from visitors.file_visitor import FileVisitor
from elements.image_file import ImageFile
from elements.document_file import DocumentFile
import os

class ConvertToPDFVisitor(FileVisitor):
    def __init__(self, output_path: str, page_size: str = "A4"):
        super().__init__(output_path)
        self._page_size = page_size
    
    def visit_image_file(self, file: ImageFile) -> None:
        print(f"Создание PDF из изображения {file.path}")
        print(f"   Параметры: {file.width}x{file.height}")
        print(f"   Размер страницы: {self._page_size}")
        
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(self._output_path, f"{name_without_ext}.pdf")
        
        print(f"   Сохранение в: {output_file}")
        
        try:
            from PIL import Image
            img = Image.open(file.path)
            img.save(output_file, 'PDF', resolution=100.0)
            print(f"   Изображение конвертировано в PDF")
        except ImportError:
            print("   Установите Pillow: pip install Pillow")
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(f"PDF конвертация: {file.path}\n")
                f.write(f"Размер страницы: {self._page_size}\n")
            print(f"   Создан файл: {output_file}")
        except Exception as e:
            print(f"   Ошибка: {e}")
    
    def visit_document_file(self, file: DocumentFile) -> None:
        print(f"Конвертация документа {file.path} в PDF")
        print(f"   Параметры: {file.pages} стр., {file.word_count} слов")
        print(f"   Размер страницы: {self._page_size}")
        
        filename = os.path.basename(file.path)
        name_without_ext = os.path.splitext(filename)[0]
        output_file = os.path.join(self._output_path, f"{name_without_ext}.pdf")
        
        print(f"   Сохранение в: {output_file}")
        
        with open(output_file, 'w', encoding='utf-8') as f:
            f.write(f"Документ конвертирован в PDF\n")
            f.write(f"Исходный файл: {file.path}\n")
            f.write(f"Страниц: {file.pages}\n")
            f.write(f"Слов: {file.word_count}\n")
            f.write(f"Размер страницы: {self._page_size}\n")
        
        print(f"   Создан файл: {output_file}")