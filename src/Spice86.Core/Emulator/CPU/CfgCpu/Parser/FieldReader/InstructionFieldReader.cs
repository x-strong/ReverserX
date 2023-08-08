namespace Spice86.Core.Emulator.CPU.CfgCpu.Parser.FieldReader;

using Spice86.Core.Emulator.CPU.CfgCpu.ParsedInstruction;
using Spice86.Core.Emulator.CPU.CfgCpu.Parser.InstructionReader333;
using Spice86.Core.Emulator.Memory.Indexable;
using Spice86.Shared.Emulator.Memory;

public abstract class InstructionFieldReader<T> {
    protected IIndexable Memory { get; }

    protected InstructionReaderAddressSource AddressSource { get; }


    protected InstructionFieldReader(IIndexable memory, InstructionReaderAddressSource addressSource) {
        Memory = memory;
        AddressSource = addressSource;
    }

    protected SegmentedAddress CurrentAddress => AddressSource.CurrentAddress;

    protected abstract int FieldSize();
    public abstract T PeekValue();

    public void Advance() {
        AddressSource.IndexInInstruction += FieldSize();
    }

    public InstructionField<T> PeekField() {
        T value = PeekValue();
        return new InstructionField<T>(AddressSource.IndexInInstruction, FieldSize(),
            CurrentAddress.ToPhysical(), value, PeekData(FieldSize()));
    }

    public InstructionField<T> NextField() {
        InstructionField<T> res = PeekField();
        Advance();
        return res;
    }


    private IList<byte?> PeekData(int size) {
        byte[] data = Memory.GetData(CurrentAddress.ToPhysical(), (uint)size);
        IList<byte?> res = new List<byte?>((int)size);
        for (int i = 0; i < data.Length; i++) {
            res.Add(data[i]);
        }

        return res;
    }
}